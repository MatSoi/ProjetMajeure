using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/**
 * Classe de gestion de la communication avec la scène. Reception des ordres de l'UDP MJ et de l'UDP Calibration.
 **/ 
public class Scene : MonoBehaviour
{
    public GameObject udpReceiver;//Reference vers le receiver UDP MJ
	public GameObject udp2CppReceiver;//Reference vers le receiver UDP de l'application de Calibration

    private string order;
    Orders currentOrder = new Orders();

	private string calibrationString;
	CalibrationOrders currentCalibrationOrder;

    // Update is called once per frame
    void Update()
    {
        if (udpReceiver.GetComponent<UDPReceive>().gameState == GameStates.GamePhase)//Si la connection UDP MJ est lancée
            treatOrder();//Traite l'ordre en provenance du MJ
		if (udp2CppReceiver.GetComponent<UDP2CppReceive>().gameState == GameStates.GamePhase)//Si la connection UDP Calibration est lancée
			treatCalibrationData();//Traite l'ordre en provenance de la calibration
    }


	/**
	 * Fonction de traitement de l'ordre en provenance du MJ, séparation de l'ordre selon les '/' et extractions des sous strings
	 * Creation d'un ordre avec les sous éléments du string recu sur l'UDP
	 **/ 
    void treatOrder()
    {
        order = udpReceiver.GetComponent<UDPReceive>().UDPGetPacket();
        string[] orders = order.Split('/');

        if (orders.Length > 1)
            if (currentOrder.nbrOrder != int.Parse(orders[0]))
            {
                currentOrder = new Orders(orders);
                currentOrder.doOrder();
            }
    }

	/**
	 * Fonction de traitement de l'ordre en provenance de la calibration, séparation du string recu selon les '_' et extraction des sous ordres
	 * Creation d'un ordre de calibration avec les sous ordres recus sur l'UDP
	 **/
	void treatCalibrationData()
	{
		calibrationString = udp2CppReceiver.GetComponent<UDP2CppReceive>().UDPGetPacket();
		string[] calibrationOrders = calibrationString.Split('_');
		if (calibrationOrders.Length >= 1)
		{
			currentCalibrationOrder = new CalibrationOrders(calibrationOrders);
			currentCalibrationOrder.doCalibrationOrders();
		}
	}
}
