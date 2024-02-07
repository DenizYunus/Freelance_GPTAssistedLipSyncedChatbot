using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OpenAITTSHelper : MonoBehaviour
{
    public static OpenAITTSHelper instance;

    private const string ApiUrl = "https://api.openai.com/v1/audio/speech";
    private const string ApiKey = "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";

    public AudioSource audioSource;
    private string currentAudioPath = "";

    [Serializable]
    public class SpeechRequest
    {
        public string model;
        public string input;
        public string voice;
        public string response_format;
        public float speed;
    }

    private void Start()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    public void Speak(string speech)
    {
        StartCoroutine(CreateSpeech(speech, "nova"));
    }

    public void Speak(InputField inputField)
    {
        StartCoroutine(CreateSpeech(inputField.text, "nova"));
    }

    private IEnumerator CreateSpeech(string text, string voice)
    {
        SpeechRequest requestParams = new SpeechRequest
        {
            model = "tts-1",
            input = text,
            voice = voice,
            response_format = "mp3", // Optional, can be omitted
            speed = 0.9f // Optional, can be omitted
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
                Debug.LogWarning($"Error: {webRequest.error}");
                Debug.LogWarning($"Response: {webRequest.downloadHandler.text}"); // This line will log the detailed error message from the server
            }
            else
            {
                Debug.LogWarning("Yup, got here");
                SaveAndPlayAudio(webRequest.downloadHandler.data);
            }
        }
    }


    private void SaveAndPlayAudio(byte[] audioData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "speech.mp3");
        File.WriteAllBytes(filePath, audioData);
        currentAudioPath = filePath;
        print(currentAudioPath);

        StartCoroutine(LoadAndPlayAudio(filePath));
    }

    private IEnumerator LoadAndPlayAudio(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();

                StartCoroutine(WaitForAudioToEnd());
            }
        }
    }
    private IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        DeleteAudioFile();
    }

    private void DeleteAudioFile()
    {
        if (File.Exists(currentAudioPath))
        {
            File.Delete(currentAudioPath);
            currentAudioPath = "";
        }
    }
}