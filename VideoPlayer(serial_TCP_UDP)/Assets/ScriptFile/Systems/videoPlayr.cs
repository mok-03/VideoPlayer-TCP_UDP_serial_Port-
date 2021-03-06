using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class videoPlayr : MonoBehaviour
{

    //private string videoPlayURL;
    public VideoPlayer uivideoPlayer;
    private Queue<string> videoPlayURL = null;
    private readonly string VideoBaseURL = "Assets/Resources/vidoPlayer/M1.mp4";
    //Maiking option add EX( Loop,speed,VideoEndFunc(overloading

    private void Start()
    {
        videoPlayURL = new Queue<string>();
     //   videoPlayURL.Enqueue(VideoBaseURL); //BaseVideo
        uivideoPlayer.url = VideoBaseURL;
    }

    public void UpdateClip() //Playing Video setting the URL
    {
        if(videoPlayURL.Count != 0)
            uivideoPlayer.url = videoPlayURL.Dequeue(); 
        else
            uivideoPlayer.url = VideoBaseURL;
      
    }

    public void InputData(Dictionary<string, string> str) //Find Video Name and videoplayer set video URL
    {
        var Vdata = Multi.xml.Key.Find(data => data.Keyvalue == (str["TextData"]));
        if (Vdata != null)
            videoPlayURL.Enqueue (Application.streamingAssetsPath + "/vidoPlayer/" + Vdata.videoName + ".mp4");

    }
}