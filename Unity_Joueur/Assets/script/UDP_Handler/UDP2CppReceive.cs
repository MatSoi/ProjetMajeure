using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

/**
 * Classe UDP2CppReceive
 * Demmarre en arriere plan le programme en CPP responsable de la calibration
 * Etabli la liaison UDP locale avec ce programme
 * Fonction d acces aux packets recus : public string UDPGetPacket()
 * */
public class UDP2CppReceive : MonoBehaviour
{
    public GameStates gameState;    // Assure que la scene ne considere pas les messages sans connexion valide
        
    static UdpClient udpClient;     // Module UDP
    static IPEndPoint ep;           // Remote UDP associe a IP et Port
    static string returnData = "";  // String recu en UDP
    Thread receiveThread;           // Thread de reception de l UDP
    Process process;                // Process de lancement du code CPP de calibration
    //int nbrSignal = 0;              // valeur de test -- a supprimer

    /**
     * Initialise la remote UDP et lance le programme CPP
     * */
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

    /**
     * Fonction liee au bouton Connect To Cpp du Menu Cpp
     * Demarre le thread reception UDP
     * */
    public void connectToCpp()
    {
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
        receiveThread.Abort();
    }

    /**
     * Thread de reception UDP
     * */
    void ReceiveData()
    {
        while (true)
        {
            byte[] receiveBytes = udpClient.Receive(ref ep);    // fonction Unity de reception, appelle bloquant
            gameState = GameStates.GamePhase;                   // si la reception est bonne, on peut considerer les packets recus
            returnData = Encoding.UTF8.GetString(receiveBytes);
            //print(returnData);
            //nbrSignal++;
        }
    }

    /**
     * Fonction publique d acces aux packets recus
     * */
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
        process.StartInfo.FileName = StartupPath + "/serveur_udp.exe";  // l exe doit se trouver dans le dossier de lancement de l application
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;      // la fenetre du programme n est pas affichee
        process.Start();
    }
}