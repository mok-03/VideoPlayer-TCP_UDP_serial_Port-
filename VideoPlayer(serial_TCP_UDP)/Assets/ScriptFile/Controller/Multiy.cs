using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public abstract class Multiy
{

   // readonly public int SemaphoreMaxCount = 10;
    public delegate void NetEvent(string str);
    private NetEvent nevent = null;
    protected bool threadEnt = false;
    protected Byte[] bytes = new Byte[256];
    protected string Text = null;
    protected Thread thread = null;
    public string GetText { get { return Text; } }
    public NetEvent SetEvent { set { nevent = value; } } // Get Key Play Func
    static public XMLData xml;

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


    public virtual void Begin(NetEvent @event)
    {
        nevent = @event;
        thread = new Thread(new ThreadStart(Reseve));
        thread.Start();
    }
    public abstract void End();
    public virtual void Reseve()
    {
        nevent(Text);
    }
    public virtual void Send() { }

}

