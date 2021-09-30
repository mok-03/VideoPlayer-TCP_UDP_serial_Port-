using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;


[SerializeField]
public class XMLData
{
    public List<videoData> Key = null;
    public NetPortdata netPortdata = null;
    public List<SerialPortOptionData> serialPortOptionData = null;

    public void MakeData()
    {
        Key = new List<videoData>();
        netPortdata = new NetPortdata();
        serialPortOptionData = new List<SerialPortOptionData>();
    }
}
public class NetPortdata
{
    public int TcpConectionNum;
    public int TCPportNumber;
    public int UDPportNumber;
    public string TCPExitKey;
}

public class SerialPortOptionData
{
    public string COMPort;
    public int BaudRate;
    public int DataBits;
    public Parity Parity;
    public StopBits StopBits;
}

public class videoData
{
    public string Keyvalue;
    public string videoName;
}
