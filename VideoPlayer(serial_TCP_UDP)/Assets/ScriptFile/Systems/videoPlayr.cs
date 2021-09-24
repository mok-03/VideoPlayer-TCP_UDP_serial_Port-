using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class videoPlayr :MonoBehaviour
{ 
    public  readonly int BaseplayVideoNumber = 0;

    public List<string> vidoPlayList;
     public int playVideoNumber = 0;
     public bool clipupdate = false;
    public VideoPlayer uivideoPlayer;
    public VideoEventSystem videoEventSystem;

    private void Start()
    {
        int i = 0;
        videoEventSystem = new VideoEventSystem();
        videoEventSystem.video = this;
        foreach(var data in Multiy.xml.Key)
        {
           
            vidoPlayList.Add(Application.streamingAssetsPath + "/vidoPlayer/" + data.videoName + ".mp4");
            videoEventSystem.DictionarySetting(data.Keyvalsue, i++);
        }
    }

    public void UpdateClip()
    {
       
        if (playVideoNumber > Multiy.xml.Key.Count)
        {
            Debug.LogWarning("숫자값이 너무큼");
            playVideoNumber = 0;
        }
        else
        {
            clipupdate = true;
        }

    }
    private void Update()
    {
        if (clipupdate == true)
        {
            uivideoPlayer.url  = vidoPlayList[playVideoNumber];
            clipupdate = false;
        }
    }

}