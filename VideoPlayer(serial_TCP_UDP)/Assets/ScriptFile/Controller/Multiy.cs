using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public abstract class Multiy
{

    readonly public int SemaphoreMaxCount = 10;
    public delegate void NetEvent(string str);
    public NetEvent nevent;
    protected bool threadEnt = false;
    protected Byte[] bytes = new Byte[256];
    protected string Text = null;
    protected Thread thread = null;
    public string GetText { get { return Text; } }
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

public class A : Multiy
{
    public override void Begin(NetEvent @event)
    {
        base.Begin(@event);
    }

    public override void End()
    {

    }

    public override void Reseve()
    {
        Text = "10";
    }
    public void NetTextEvent(string str)
    {

    }
}

public class M
{

    A a;
    public void C()
    {
        a = new A();


        // a.Begin(NetTextEvent(Text)); //begin에서 this(a)의 
    }
}

