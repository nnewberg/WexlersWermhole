using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour {

    public VideoClip Clip;
    public string URL;
    public AudioSource AudioSource;
    public VideoPlayer videoPlayer;

    void Awake()
    {
        //Subscribe to pickup and dropping events on this object
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(Play);
        obj.onDetachFromHand.AddListener(Stop);
        obj.onGazeTrigger.AddListener(Play);
        obj.onGazeExit.AddListener(Stop);

        //videoPlayer = GetComponent<VideoPlayer>();
        if (URL.Length > 0) //if there's a URL, use that as the source
        {
            videoPlayer.url = URL;
            videoPlayer.source = VideoSource.Url;
        }else //otherwise, look for a local clip
        {
            videoPlayer.clip = Clip;
            videoPlayer.source = VideoSource.VideoClip;

        }
        videoPlayer.SetTargetAudioSource(0, AudioSource);

        //Creating a 'psuedo-thumbnail'
        videoPlayer.frame = (long) videoPlayer.frameCount / 2L;

        //KINDA SKETCH
        videoPlayer.Play();
        videoPlayer.Pause();
    }

    private void Play()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.frame = 0L;
            videoPlayer.Play();
        }
    }

    private void Stop()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount / 2L;
        videoPlayer.Pause();
    }


}
