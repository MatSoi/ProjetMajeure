using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class UDPReceive : MonoBehaviour {
    Thread receiveThread;
    UdpClient client;
	public InputField IP;
    public InputField port;
    public GameObject menu;
    string strReceiveUDP = "";
    bool reception = false;
    public GameObject outText;
    public GameStates gameState;

    public void Start() {
        gameState = GameStates.StartPhase;
        Application.runInBackground = true;
    }

    public void Update() {
        if (reception) {
            reception = false;
            printOut(strReceiveUDP);
        }
    }

    public void connectClient() {
        closeThreadUDP();
        printOut("Client Connection");
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData() {
        client = new UdpClient(int.Parse(port.text));
		
        while (true) {
            try {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(port.text));
                byte[] data = client.Receive(ref remoteEndPoint);
                string text = Encoding.UTF8.GetString(data);
                strReceiveUDP = text;
                reception = true;
                //Debug.Log(strReceiveUDP);
            }
            catch (Exception err) {
                print(err.ToString());
            }
        }
    }

    public string UDPGetPacket() {
        return strReceiveUDP;
    }

    void OnApplicationQuit() {
        closeThreadUDP();
    }

    void closeThreadUDP() {
        if(receiveThread != null) {
            if (receiveThread.IsAlive) {
                receiveThread.Abort();
            }
        }
        if (client != null) client.Close();
    }

	private void printOut (string myText) {
        outText.GetComponent<Animation> ().Stop ();
		outText.GetComponentInChildren<Text>().text = myText;
		outText.GetComponent<Animation> ().Play ();
	}

    public void closeOptions() {
        gameState = GameStates.GamePhase;
        menu.SetActive(false);
    }
}