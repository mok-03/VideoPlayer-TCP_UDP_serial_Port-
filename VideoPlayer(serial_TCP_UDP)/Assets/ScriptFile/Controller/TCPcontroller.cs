using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPcontroller : Multi
{
    static TcpListener server = null;

    private bool conecting;
    private TcpClient client;
    static private object TCPKEY = new object();
    NetworkStream stream;
    public override void Begin(NetEvent @event)
    {

        createServer();
        base.Begin(@event);
    }

    public override void End()
    {
        croseServer();
    }

 //MinEvet->BeginFunc->base.Begin->Reseve(Thread);
    public override void Reseve()
    {
        if (conecting != true)
        {
            clientConecting();
        }

        while (!threadEnt)
        {
            Thread.Sleep(1);

            GetClientMessage();
            if (Text != null)
            {
                base.Reseve();
                Text = null;
            }
        }

    }

    void createServer()
    {
        try
        {
            if (server == null)
            {
                IPAddress localAddr = IPAddress.Parse(ComputerIP);
                Debug.Log("My server ip" + localAddr);
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
        if (stream.CanRead && stream.DataAvailable)
        {
            Int32 size = stream.Read(bytes, 0, bytes.Length);
            Text = System.Text.Encoding.ASCII.GetString(bytes, 0, size);
            if(Text == xml.netPortdata.TCPExitKey)
            {
                Debug.Log(Text);
                resetting();
                Text = null;
            }
            //   Text = Text.ToUpper(); //대문자치환
        }
    }

    //crose the server
    void croseServer()
    {
        threadEnt = true;
        if (client != null)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("deconect(server)");
            if (stream != null)
            {
                stream.Write(msg, 0, msg.Length);
                stream.Close();
            }
        }
        conecting = false;
        thread.Join();
    }


    void clientConecting()
    {
        while (!threadEnt && client == null)
        {
            try
            {
                lock (TCPKEY) //LOCK말고 STATIC LIST사용도 나쁘지 않아보임
                {
                    if (server.Pending())
                    {//get client 
                        client = server.AcceptTcpClient(); 
                    }
                }
            }
            catch (SocketException e) when (e.SocketErrorCode == SocketError.Interrupted)
            {
                throw new OperationCanceledException();

            }
        }


        if (!threadEnt && client != null)
        {
            Debug.Log("connected..");
            stream = client.GetStream();
            Socket c = client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            uniquenIP = ip_point.Address.ToString();
            conecting = true;
        }
    }
    public void resetting()
    {
        client.Close();
        client = null;
        clientConecting();

    }
    static public void serverstop()
    {
        server.Stop();

    }

}
