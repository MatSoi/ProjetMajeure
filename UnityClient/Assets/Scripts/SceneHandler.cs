using UnityEngine;

public class SceneHandler : MonoBehaviour {
    public GameObject udpSender;
    private int nbrOrder = 0;

    public void sendOrder () {
        nbrOrder++;
        udpSender.GetComponent<UDPSend>().sendString(nbrOrder.ToString() + "/R/Player1/0/15/0");
    }
	
}
