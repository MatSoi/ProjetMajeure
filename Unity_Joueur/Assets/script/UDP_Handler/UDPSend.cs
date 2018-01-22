using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class UDPSend : MonoBehaviour {
    public InputField IP;
    public InputField port;
    public GameObject outText;
    IPEndPoint remoteEndPoint;
    UdpClient client;
    string strMessage = "";
    int test = 0;

    public void connectServer() {
        closeServer();
        printOut("Server Connection");
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(port.text));
        client = new UdpClient();
    }

    public void sendString(string message) {
        try {
            strMessage = message;
            byte[] data = Encoding.UTF8.GetBytes(strMessage);
            client.Send(data, data.Length, remoteEndPoint);
            printOut(strMessage);
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