using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using UnityEngine.UI;

public class UDP2CppReceive : MonoBehaviour
{
    static IPEndPoint ep;
    static UdpClient udpClient;
    static byte[] receiveBytes = new byte[32];
    static string returnData = "";
    Thread receiveThread;

	public GameStates gameState;
    //public GameObject signal;

    Process process;

    int nbrSignal = 0;

    void Start()
    {
		gameState = GameStates.StartPhase;
        ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        udpClient = new UdpClient(8888);
        StartServer();
        //signal.GetComponentInChildren<Text>().text = Application.dataPath;
    }

    void Update()
    {
        //signal.GetComponentInChildren<Text>().text = Application.dataPath + " " + nbrSignal.ToString();
    }

    public void connectToCpp()
    {
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
        receiveThread.Abort();
    }

    void ReceiveData()
    {
		gameState = GameStates.GamePhase;
        while (true)
        {
            receiveBytes = udpClient.Receive(ref ep);
            returnData = Encoding.UTF8.GetString(receiveBytes);
            print(returnData);
            nbrSignal++;
        }
    }

	public string UDPGetPacket() {
		return returnData;
	}

    void OnApplicationQuit()
    {
        if (receiveThread != null)
            if (receiveThread.IsAlive)
                receiveThread.Abort();
    
        if (udpClient != null) udpClient.Close();

        process.Kill();
    }

    void StartServer()
    {
        string StartupPath = Application.dataPath;
        process = new Process();
        process.StartInfo.FileName = StartupPath + "/serveur_udp.exe";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

        process.Start();
    }
}