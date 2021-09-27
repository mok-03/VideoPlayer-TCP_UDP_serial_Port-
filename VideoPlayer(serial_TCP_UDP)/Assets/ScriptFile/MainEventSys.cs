using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class MainEventSys : MonoBehaviour
{
    static MainEventSys mainEventSys = null;
    static NetUIEventMenager netUIEventMenager = null;
    // Start is called before the first frame update
    public videoPlayr videoPlayer;

    private static string XmlFilelocation;

    UDPcontroller UDP = null;
    Serialcontroller serial = null;
    TCPcontroller TCP = null;



    private void Awake()
    {
        if (mainEventSys == null)
        {
            mainEventSys = this;
            DontDestroyOnLoad(gameObject);
        }


        XmlFilelocation = Application.streamingAssetsPath + "/XML/XMLData.xml";
        Multi.xml = new XMLData();
        Multi.xml.MakeData();
        Multi.xml = XMLPaser.Load<XMLData>(XmlFilelocation, Multi.xml);
        if (Multi.xml == null)
        {

            Multi.xml = new XMLData();
            Multi.xml.MakeData();
            Multi.xml.netPortdata.TCPportNumber = 8882;
            Multi.xml.netPortdata.UDPportNumber = 887;
            Multi.xml.serialPortOptionData.COMPort = "COM11";
            Multi.xml.serialPortOptionData.BaudRate = 9600;
            Multi.xml.serialPortOptionData.DataBits = 8;
            Multi.xml.serialPortOptionData.Parity = Parity.None;
            Multi.xml.serialPortOptionData.StopBits = StopBits.One;
            videoData data = new videoData();
            data.Keyvalue = "1";
            data.videoName = "M1";
            Multi.xml.Key.Add(data);
            XMLPaser.Save<XMLData>(XmlFilelocation, Multi.xml);
        }

        InitNetController();

        if (netUIEventMenager == null)
            netUIEventMenager = new NetUIEventMenager();

        netUIEventMenager.Begin();

    }

    // Update is called once per frame
    private void Update()
    {
        if (netUIEventMenager.checkQueueStack())
            netUIEventMenager.UpdateQueue();
    }
    void OnApplicationQuit()
    {
        EndNetController();
    }

    private void InitNetController()
    {
        UDP = new UDPcontroller();
        serial = new Serialcontroller();
        TCP = new TCPcontroller();
       
            UDP.Begin(NetTextEvent);
            // open the Port


        serial.Begin(NetTextEvent);
        TCP.Begin(NetTextEvent);


    }
    private void EndNetController()
    {
        UDP.End();
        serial.End();
        TCP.End();
    }

    public void NetTextEvent(string str)
    {

        Debug.Log(str);
        videoPlayer.InputData(str); 
        netUIEventMenager.SetFuncs(VideoEvent); 

    }

    public void VideoEvent()
    {
        videoPlayer.UpdateClip();
    }


}
