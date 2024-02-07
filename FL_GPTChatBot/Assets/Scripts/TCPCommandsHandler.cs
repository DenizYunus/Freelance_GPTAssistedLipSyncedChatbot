using GoogleCloudStreamingSpeechToText;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPCommandsHandler : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    // Define command classes to parse JSON
    [Serializable]
    private class Command
    {
        public string type;
        public string message;
    }

    void Start()
    {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void ListenForIncomingRequests()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 3169);
            tcpListener.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            string clientMessage = System.Text.Encoding.ASCII.GetString(incomingData);
                            Debug.Log("client message received as: " + clientMessage);
                            try
                            {
                                Command command = JsonUtility.FromJson<Command>(clientMessage);
                                HandleCommand(command);
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError("Error parsing command: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.LogError("SocketException " + socketException.ToString());
        }
    }

    private void HandleCommand(Command command)
    {
        switch (command.type)
        {
            case "setLanguage":
                UnityMainThreadDispatcher.Instance().Enqueue(() => StreamingRecognizerEdited.instance.languageCode = command.message);
                Debug.Log($"Setting language to: {command.message}");
                break;
            case "speech":
                Debug.Log($"Handling speech command: {command.message}");
                UnityMainThreadDispatcher.Instance().Enqueue(() => OpenAITTSHelper.instance.Speak(command.message));
                break;
            case "recognizer":
                if (command.message == "start")
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => StreamingRecognizerEdited.instance.StartListening());
                    break;
                }
                else if (command.message == "stop")
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => StreamingRecognizerEdited.instance.StopListening());
                    break;
                }
                break;
            default:
                Debug.Log("Unknown command received.");
                break;
        }
    }

    private void OnApplicationQuit()
    {
        if (tcpListener != null)
        {
            tcpListener.Stop();
        }
    }
}
