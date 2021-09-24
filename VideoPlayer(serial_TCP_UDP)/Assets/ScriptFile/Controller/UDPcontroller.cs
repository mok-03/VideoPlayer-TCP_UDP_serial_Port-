using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPcontroller : Multiy
{
    UdpClient server = null;
    public override void Begin(NetEvent @event)
    {
        server = new UdpClient(xml.netPortdata.UDPportNumber);
        base.Begin(@event);
    }

    public override void End()
    {
        threadEnt = true;
        if (thread.IsAlive)
            thread.Abort();
        if (server != null)
            server.Close();
    }

    public override void Reseve()
    {
        IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
        while (!threadEnt)
        {
            bytes = server.Receive(ref epRemote);
            Text = Encoding.ASCII.GetString(bytes);
            base.Reseve();
        }

    }

}
