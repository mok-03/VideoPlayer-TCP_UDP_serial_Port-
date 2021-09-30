using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPcontroller : Multi
{
    UdpClient server = null;
    public override void Begin(NetEvent @event)
    {
        server = new UdpClient(xml.netPortdata.UDPportNumber);
        base.Begin(@event);
    }

    public override void End()
    {    threadEnt = true;

        if (thread.IsAlive)
            thread.Join();
        if (server != null)
            server.Close();
    }

    public override void Reseve()
    {
        IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
        while (!threadEnt)
        {
            Thread.Sleep(1);

            if (server.Available > 0)
            {

                bytes = server.Receive(ref epRemote);

                Text = Encoding.ASCII.GetString(bytes);
                uniquenIP = epRemote.Address.ToString();

                base.Reseve();
            }
        }

    }

}
