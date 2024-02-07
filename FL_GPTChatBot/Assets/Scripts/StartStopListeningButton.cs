using GoogleCloudStreamingSpeechToText;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartStopListeningButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public StreamingRecognizerEdited recognizer;
    public Text buttonLabel;

    public void OnPointerDown(PointerEventData eventData)
    {
        recognizer.StartListening();
        buttonLabel.text = "Stop";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        recognizer.StopListening();
        buttonLabel.text = "Listen";
    }
}