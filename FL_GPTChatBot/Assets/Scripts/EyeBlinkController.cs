using System;
using UnityEngine;
using UnityEngine.Video;

public class EyeBlinkController: MonoBehaviour
{
    public VideoPlayer videoPlayer; // Attach your VideoPlayer component here
    public float minTime = 2.0f; // Minimum time between blinks
    public float maxTime = 5.0f; // Maximum time between blinks

    private float timeToNextBlink;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        SetNextBlinkTime();
    }

    void Update()
    {
        if (timeToNextBlink <= 0f)
        {
            Blink();
            SetNextBlinkTime();
        }
        else
        {
            timeToNextBlink -= Time.deltaTime;
        }
    }

    void SetNextBlinkTime()
    {
        timeToNextBlink = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Blink()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }
}
