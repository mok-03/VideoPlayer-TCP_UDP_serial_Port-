using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public abstract class Multi
{

   // readonly public int SemaphoreMaxCount = 10;
    public delegate void NetEvent(Dictionary<string, string> str);
    private NetEvent nevent = null;
    protected bool threadEnt = false;
    protected Byte[] bytes = new Byte[256];
    protected string Text = null;
    protected Thread thread = null;
    public string GetText { get { return Text; } }
    public NetEvent SetEvent { set { nevent = value; } } // Get Key Play Func
    public string uniquenIP =null;
    static public XMLData xml;
     public Dictionary<string, string> ReadData = new Dictionary<string, string>();

    //Get My IP Func
    public static string ComputerIP
    {
        get
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ClientIP = string.Empty;

            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ClientIP = host.AddressList[i].ToString();
                }
            }
            return ClientIP;
        }
    }

    //MinEvet->BeginFunc->base.Begin->Reseve(Thread);
    public virtual void Begin(NetEvent @event)
    {
        nevent = @event;
        thread = new Thread(new ThreadStart(Reseve));
        thread.Start();
    }
    public abstract void End();
    public virtual void Reseve()
    {  
        ReadData["IPData"] = uniquenIP;
        ReadData["TextData"] = Text;
        nevent(ReadData);
    }
    public virtual void Send() { }

}

