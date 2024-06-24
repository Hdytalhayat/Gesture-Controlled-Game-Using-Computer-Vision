using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    private Thread receiveThread;
    private UdpClient client;
    public int port = 1209;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void OnDestroy()
    {
        CloseSocket();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if (printToConsole)
                {
                    print(data);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void CloseSocket()
    {
        if (client != null)
        {
            client.Close();
            client = null;
        }

        if (receiveThread != null)
        {
            receiveThread.Abort();
            receiveThread = null;
        }
    }
}
