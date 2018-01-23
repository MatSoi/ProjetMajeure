using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/**
 * Communication Manager of the scene. Receiving orders from game master UDP and Calibration UDP
 **/ 
public class Scene : MonoBehaviour
{
    public GameObject udpReceiver;//Reference to game master UDP receiver
	public GameObject udp2CppReceiver;//Reference to UDP receiver for Calibration app

    private string order;
    Orders currentOrder = new Orders();

	private string calibrationString;
	CalibrationOrders currentCalibrationOrder;

    void Update()
    {
        if (udpReceiver.GetComponent<UDPReceive>().gameState == GameStates.GamePhase)//when GM UDP connection launched
            treatOrder();//treats GM orders
		if (udp2CppReceiver.GetComponent<UDP2CppReceive>().gameState == GameStates.GamePhase)//when UDP Calibration connection is launched
			treatCalibrationData();//treats orders from the calibration app
    }


	/**
     * Orders Treatment function : Treats orders from GM, orders divided by '/' and extraction of sub-strings
     * Order creation with sub-elements of UDP received string
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
     * Orders Treatment function : Treats orders from calibration, orders divided by '_' and extraction of sub-orders
     * Calibration Order creation with sub-orders received from UDP
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
