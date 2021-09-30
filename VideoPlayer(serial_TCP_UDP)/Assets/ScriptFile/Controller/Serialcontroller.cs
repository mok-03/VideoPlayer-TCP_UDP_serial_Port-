using System;
using System.IO.Ports;
using System.Threading;

public class Serialcontroller : Multi
{
    // Start is called before the first frame update
    SerialPort _serialPort;
    static int serialportNum = 0;
    public override void Begin(NetEvent @event)
    {
        _serialPort = new SerialPort();
        initport();
        base.Begin(@event);
        thread.IsBackground = true; 
        //main(UI)thread가 종료되면 같이종료됨 base(false)는 계속돌아감
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

        if (_serialPort.IsOpen) {return; }
        _serialPort.Open(); 
        serialportNum++;
    }
    public void initport()
    {
        _serialPort.PortName = xml.serialPortOptionData[serialportNum].COMPort;
        _serialPort.BaudRate = xml.serialPortOptionData[serialportNum].BaudRate;
        _serialPort.DataBits = xml.serialPortOptionData[serialportNum].DataBits;
        _serialPort.Parity = xml.serialPortOptionData[serialportNum].Parity;
        _serialPort.StopBits = xml.serialPortOptionData[serialportNum].StopBits;
 
            openPort();
        

    }

    private void SerialUpdate()
    {
        while (!threadEnt)
        {
            Thread.Sleep(1);

            if (_serialPort.IsOpen)
                Readline();

            if (Text != null)
            {
                uniquenIP = _serialPort.PortName;
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
