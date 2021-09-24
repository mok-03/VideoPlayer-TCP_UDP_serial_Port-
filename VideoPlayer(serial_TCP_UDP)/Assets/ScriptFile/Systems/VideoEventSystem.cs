using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoEventSystem 
{
   public videoPlayr video = null;
   public Dictionary<string, int> dic = null;

     public void PortInputData(string Key) //회신받ㅇ면 이거쓰면됨
    {
        try
        {
            video.playVideoNumber = dic[Key];
          
        }
        catch
        {
            video.playVideoNumber = video.BaseplayVideoNumber;

        }
    }
     public void DictionarySetting(string Key,int vlu)
    {
        if(dic == null)
        {
            dic = new Dictionary<string, int>();
        }
        dic.Add(Key, vlu);
    }



}
