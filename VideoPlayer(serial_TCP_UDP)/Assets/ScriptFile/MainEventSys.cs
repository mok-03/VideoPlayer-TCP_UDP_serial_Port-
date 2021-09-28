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
    List<Serialcontroller> serial = null;
    List<TCPcontroller> TCP = null;
    //want single TCP&serial u can Destory List



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
            Multi.xml.netPortdata.TCPportNumber = new List<int>();
            Multi.xml.netPortdata.TCPportNumber.Add(8882);
            Multi.xml.netPortdata.UDPportNumber = 887;
            SerialPortOptionData serialPortOptionData = new SerialPortOptionData();
            serialPortOptionData.COMPort = "COM11";
            serialPortOptionData.BaudRate = 9600;
            serialPortOptionData.DataBits = 8;
            serialPortOptionData.Parity = Parity.None;
            serialPortOptionData.StopBits = StopBits.One;
            Multi.xml.serialPortOptionData.Add(serialPortOptionData);

           

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

        serial = new List<Serialcontroller>();
        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial.Add(new Serialcontroller());

        TCP = new List<TCPcontroller>();
        for (int i = 0; i < Multi.xml.netPortdata.TCPportNumber.Count; i++)
            TCP.Add(new TCPcontroller());





        UDP.Begin(NetTextEvent); // Anycast

        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial[i].Begin(NetTextEvent);

        for (int i = 0; i < Multi.xml.netPortdata.TCPportNumber.Count; i++)
            TCP[i].Begin(NetTextEvent);


    }
    private void EndNetController()
    {
        UDP.End();

        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial[i].End();

        for (int i = 0; i < Multi.xml.netPortdata.TCPportNumber.Count; i++)
            TCP[i].End();
    }

    public void NetTextEvent(string str)
    {

        Debug.Log(str);  
        //videoplayer 두개 사용할려면 여기에 videEnvet1,videoplayer1 을 하나더만들고
       //bool로 확인후 번갈아가면서 인덱스 추가하면 
        videoPlayer.InputData(str);
        netUIEventMenager.SetFuncs(VideoEvent);


    }

    public void VideoEvent()
    {
        videoPlayer.UpdateClip();
    }


}
