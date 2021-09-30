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
        //singleton pattern
        if (mainEventSys == null)
        {
            mainEventSys = this;
            DontDestroyOnLoad(gameObject);
        }


        XmlFilelocation = Application.streamingAssetsPath + "/XML/XMLData.xml";
        Multi.xml = new XMLData();
        Multi.xml.MakeData();
        Multi.xml = XMLPaser.Load<XMLData>(XmlFilelocation, Multi.xml);

        #region MissingXML created BaseData Input and Save
        if (Multi.xml == null)
        {

            Multi.xml = new XMLData();
            Multi.xml.MakeData();
            Multi.xml.netPortdata.TCPportNumber = (8882);
            Multi.xml.netPortdata.TcpConectionNum = 1;
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
        #endregion
        InitNetController();

        if (netUIEventMenager == null)
            netUIEventMenager = new NetUIEventMenager();

        netUIEventMenager.Begin();

    }

    // Update is called once per frame
    private void Update()
    {
        if (netUIEventMenager.checkQueueStack())
            netUIEventMenager.UpdateQueue(); // output MinaEventQueue
    }
    void OnApplicationQuit()
    {
        EndNetController();
    }

    private void InitNetController()
    {
        #region create new classes
        UDP = new UDPcontroller();
        serial = new List<Serialcontroller>();
        TCP = new List<TCPcontroller>();

        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial.Add(new Serialcontroller());


        for (int i = 0; i < Multi.xml.netPortdata.TcpConectionNum; i++)
            TCP.Add(new TCPcontroller());
        #endregion

        #region BeginFunc
        UDP.Begin(NetTextEvent); // Anycast

        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial[i].Begin(NetTextEvent);

        for (int i = 0; i < Multi.xml.netPortdata.TcpConectionNum; i++)
            TCP[i].Begin(NetTextEvent);
        #endregion

    }
    private void EndNetController()
    {

        UDP.End();

        for (int i = 0; i < Multi.xml.serialPortOptionData.Count; i++)
            serial[i].End();

        for (int i = 0; i < Multi.xml.netPortdata.TcpConectionNum; i++)
            TCP[i].End();

        TCPcontroller.serverstop();
    }

    public void NetTextEvent(Dictionary<string, string> str)
    {

        // Debug.Log(str.ContainsKey("TextData"));  
        Debug.Log(str["IPData"]);
        Debug.Log(str["TextData"]);

        videoPlayer.InputData(str); //FindKey input Queue
        netUIEventMenager.SetFuncs(VideoEvent); //Func Input Main EventQueue 


    }

    public void VideoEvent()
    {
        videoPlayer.UpdateClip(); //videoplayerObject input Video Queue URL
    }


}
