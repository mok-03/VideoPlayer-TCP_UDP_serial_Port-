using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TCPcontroller : Multi
{
    TcpListener server = null;
    private bool conecting;
    NetworkStream stream;

    public override void Begin(NetEvent @event)
    {
        createServer();
        base.Begin(@event); //리시브 스레드 실행
    }

    public override void End()
    {
        croseServer();
    }

    public override void Reseve()
    {
        if (conecting != true)
        {
            TcpClient client = server.AcceptTcpClient();
            Debug.Log("connected..");
            conecting = true;
            stream = client.GetStream();
        }

        while (!threadEnt)
        {
            GetClientMessage();
        }

    }

    void createServer()
    {
        try
        {
            if (server == null)
            {
                IPAddress localAddr = IPAddress.Parse(ComputerIP);
                Debug.Log("this ip" + localAddr);
                server = new TcpListener(localAddr, xml.netPortdata.TCPportNumber);
                server.Start();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error code :" + e + "\n TCPPORT.cs");
        }

    }


    void GetClientMessage()
    {

        int i;
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) // stream.Read(내부에서 돌다가 새로운데이터가 들어오면 나오는거같음
        {
            Text = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            Text = Text.ToUpper();
            base.Reseve();
        }


    }

    void croseServer()
    {

        threadEnt = true;
        byte[] msg = System.Text.Encoding.ASCII.GetBytes("deconect(server)");
        if (stream != null)
        {
            stream.Write(msg, 0, msg.Length);
            stream.Close();
        }
        thread.Abort();
        server.Stop();
        conecting = false;

    }

}
