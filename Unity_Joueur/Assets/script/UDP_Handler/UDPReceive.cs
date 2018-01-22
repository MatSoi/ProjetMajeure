using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

/**
 * Classe UDPReceive
 * Demarre une connexion UDP en thread separe.
 * Fonction d acces aux messages recus : public string UDPGetPacket()
 * */
public class UDPReceive : MonoBehaviour {
	public InputField IP;           // IP du server
    public InputField port;         // Port du server
    public GameObject outText;      // Popup de verification, affiche le message recu a l ecran
    public GameStates gameState;    // Assure que la scene ne considere pas les messages sans connexion valide

    Thread receiveThread;           // Thread de reception des packets UDP
    UdpClient client;               // Module UDP
    string strReceiveUDP = "";      // Message recu par la liaison UDP
    bool reception = false;         // Permet l affichage du popup seuelement une fois par reception

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

    /**
     * Fonction lier au bouton connect du Menu UDP Client
     * Ferme la liaison UDP si elle existait deja et en ouvre une autre
     * */
    public void connectClient() {
        closeThreadUDP();
        printOut("Client Connection");
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    /**
     * Thread de reception du packet UDP
     * */
    private void ReceiveData() {
        client = new UdpClient(int.Parse(port.text));
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(port.text));

        while (true) {
            try {
                byte[] data = client.Receive(ref remoteEndPoint);   // appelle de reception bloquant
                gameState = GameStates.GamePhase;                   // si la reception a reussi, le jeu passe en gamephase
                string text = Encoding.UTF8.GetString(data);
                strReceiveUDP = text;
                reception = true;
            }
            catch (Exception err) {
                print(err.ToString());
            }
        }
    }

    // Fonction public permettant d acceder au packet recu
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
}