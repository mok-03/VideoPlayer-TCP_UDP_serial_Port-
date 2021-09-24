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
            DontDestroyOnLoad(this.gameObject);
        }


        XmlFilelocation = Application.streamingAssetsPath + "/XML/XMLData.xml";
        Multiy.xml = new XMLData();
        Multiy.xml.MakeData();
        Multiy.xml = XMLPaser.Load<XMLData>(XmlFilelocation, Multiy.xml);
        if (Multiy.xml == null)
        {

            Multiy.xml = new XMLData();
            Multiy.xml.MakeData();
            Multiy.xml.netPortdata.TCPportNumber = 8882;
            Multiy.xml.netPortdata.UDPportNumber = 887;
            Multiy.xml.serialPortOptionData.COMPort = "COM11";
            Multiy.xml.serialPortOptionData.BaudRate = 9600;
            Multiy.xml.serialPortOptionData.DataBits = 8;
            Multiy.xml.serialPortOptionData.Parity = Parity.None;
            Multiy.xml.serialPortOptionData.StopBits = StopBits.One;
            videoData data = new videoData();
            data.Keyvalsue = "1";
            data.videoName = "M1";
            Multiy.xml.Key.Add(data);
            XMLPaser.Save<XMLData>(XmlFilelocation, Multiy.xml);
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
        videoPlayer.videoEventSystem.PortInputData(str);
        //ui전환 이벤트 큐 추가 딕셔너리로 호출 이벤트 사용 또는 영상물 이름으로 호출 URL
        netUIEventMenager.SetFuncs(VideoEvent);

    }

    public void VideoEvent()
    {
        videoPlayer.UpdateClip();

    }


}
