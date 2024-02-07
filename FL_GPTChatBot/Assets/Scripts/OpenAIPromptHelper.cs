using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIPromptHelper : MonoBehaviour
{
    private const string ApiUrl = "https://api.openai.com/v1/chat/completions";
    private const string ApiKey = "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";

    [Serializable]
    public class ChatRequest
    {
        public string model;
        public Message[] messages;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    private void Start()
    {

    }

    public void GetResponse(string prompt)
    {
        StartCoroutine(SendRequestToGPT3_5Turbo(prompt));
    }

    private IEnumerator SendRequestToGPT3_5Turbo(string prompt)
    {
        ChatRequest requestParams = new ChatRequest
        {
            model = "gpt-3.5-turbo",
            messages = new Message[]
            {
                new Message { role = "system", content = "You are a helpful assistant. You can give response maximum 50 words." },
                new Message { role = "user", content = prompt }
            }
        };

        string requestBody = JsonUtility.ToJson(requestParams);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(requestBody);

        using (UnityWebRequest webRequest = new UnityWebRequest(ApiUrl, "POST"))
        {
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + ApiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                HandleResponse(webRequest.downloadHandler.text);
            }
        }
    }

    [Serializable]
    public class ChatResponse
    {
        public string id;
        [SerializeField] private string _object; // Renamed to avoid conflict with the reserved keyword 'object'
        public long created;
        public string model;
        public Choice[] choices;
        public Usage usage;

        // Public property to access the private _object field
        public string Object
        {
            get { return _object; }
            set { _object = value; }
        }
    }

    [Serializable]
    public class Choice
    {
        public int index;
        public Message message;
        public string finish_reason;
    }

    [Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }

    private void HandleResponse(string jsonResponse)
    {
        ChatResponse response = JsonUtility.FromJson<ChatResponse>(jsonResponse);

        if (response != null && response.choices != null && response.choices.Length > 0)
        {
            string assistantMessage = response.choices[0].message.content;
            Debug.Log("Assistant's message: " + assistantMessage);
            OpenAITTSHelper.instance.Speak(assistantMessage);
        }
        else
        {
            Debug.LogError("Invalid response or no message from the assistant.");
        }
    }
}