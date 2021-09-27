using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class videoPlayr : MonoBehaviour
{
    public readonly int BaseplayVideoNumber = 0;

    public string videoPlayURL;


    public bool clipupdate = false;
    public VideoPlayer uivideoPlayer;


    private void Start()
    {
        videoPlayURL = ("Assets/Resources/vidoPlayer/M1.mp4"); //BaseVideo
    }

    public void UpdateClip() //Playing Video setting the URL
    {
        uivideoPlayer.url = videoPlayURL;
    }

    public void InputData(string str) //Find Video Name and videoplayer set video URL
    {
        var Vdata = Multi.xml.Key.Find(data => data.Keyvalue == (str));
        if (Vdata != null)
            videoPlayURL = (Application.streamingAssetsPath + "/vidoPlayer/" + Vdata.videoName + ".mp4");

    }
}