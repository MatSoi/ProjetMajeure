using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

/**
 * Classe UDPSend
 * Demarre une connexion UDP en thread principal (fonction send non bloquante)
 * Fonction publique d envoi de message : public void sendString(string message)
 * */
public class UDPSend : MonoBehaviour {
    public InputField IP;       // IP du client
    public InputField port;     // Port du client
    public GameObject outText;  // Popup de verification, affiche le message envoye a l ecran

    UdpClient client;           // Module UDP
    IPEndPoint remoteEndPoint;  // Remote UDP associe a IP et Port
    int test = 0;               // chiffre pour les envoi de test

    /**
    * Fonction lier au bouton connect du Menu UDP Server
    * Ferme la liaison UDP si elle existait deja et en ouvre une autre
    * */
    public void connectServer() {
        closeServer();
        printOut("Server Connection");
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(port.text));
        client = new UdpClient();
    }

    /**
     * Envoi le message en parametre par la liaison UDP
     * */
    public void sendString(string message) {
        try {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint); // fonction d envoi de Unity, non bloquante
            printOut(message);                              // popup du message envoye
        } 
		catch (Exception err) {
            print(err.ToString());
        }
    }

    void OnApplicationQuit() {
        closeServer();
    }

    void closeServer() {
        if (client != null) client.Close();
    }

    /**
     * A chaque fois que l on appuie sur le bouton Send Test du Menu Server
     * On envoi un ciffre que l'on incremente.
     * */
    public void sendTest() {
        sendString(test.ToString());
        ++test;
    }

    private void printOut (string myText) {
        outText.GetComponent<Animation> ().Stop ();
		outText.GetComponentInChildren<Text>().text = myText;
		outText.GetComponent<Animation> ().Play ();
	}
}