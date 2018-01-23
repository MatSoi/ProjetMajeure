using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Scene : MonoBehaviour
{
    public GameObject udpReceiver;
	public GameObject udp2CppReceiver;

    private string order;
    Orders currentOrder = new Orders();

	private string calibrationString;
	CalibrationOrders currentCalibrationOrder;

    public Scene()
    {
		//AndroidJavaClass jc = new AndroidJavaClass("java.lang.Object");
    }

    // Update is called once per frame
    void Update()
    {
        if (udpReceiver.GetComponent<UDPReceive>().gameState == GameStates.GamePhase)
            treatOrder();
		if (udp2CppReceiver.GetComponent<UDP2CppReceive>().gameState == GameStates.GamePhase)
			treatCalibrationData();
    }

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
