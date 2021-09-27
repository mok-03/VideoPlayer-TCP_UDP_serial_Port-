using System;
using System.IO.Ports;


public class Serialcontroller : Multi
{
    // Start is called before the first frame update
    SerialPort _serialPort;

    public override void Begin(NetEvent @event)
    {
        _serialPort = new SerialPort();
        initport();
        base.Begin(@event);
    }

    public override void End()
    {
        EndPort();
    }

    public override void Reseve()
    {
        SerialUpdate();
    }
    public void openPort()
    {

            if (_serialPort.IsOpen) return;
            _serialPort.Open(); //시리얼 오픈~
 
    }
    public void initport()
    {

        _serialPort.PortName = xml.serialPortOptionData.COMPort;
        _serialPort.BaudRate = xml.serialPortOptionData.BaudRate;
        _serialPort.DataBits = xml.serialPortOptionData.DataBits;
        _serialPort.Parity = xml.serialPortOptionData.Parity;
        _serialPort.StopBits = xml.serialPortOptionData.StopBits;
        openPort();
    }

    private void SerialUpdate()
    {
        while (!threadEnt)
        {
            if (_serialPort.IsOpen)
                Readline();
            if (Text != null)
            {
                base.Reseve();
                Text = null;
            }

        }
    }

    private void Readline()
    {
        int size = _serialPort.BytesToRead;
        if (size > 0)
        {
            _serialPort.Read(bytes, 0, size);
            Text = System.Text.Encoding.ASCII.GetString(bytes, 0, size);
        }

    }

    public void EndPort()
    {
        threadEnt = true;
        thread.Join();
        _serialPort.Close();
    }

}
