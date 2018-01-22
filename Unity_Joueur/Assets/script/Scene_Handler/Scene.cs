using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Scene : MonoBehaviour
{

    public GameObject udpReceiver;
	public GameObject udp2CppReceiver;

    [SerializeField]
	private GameObject lCamera;
    [SerializeField]
    private GameObject rCamera;
    [SerializeField]
    private GameObject board;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject player3;

    private string order;
    Orders currentOrder = new Orders();

	private string calibrationOrder;
	CalibrationOrders currentCalibrationOrder = new CalibrationOrders();

    public Scene()
    {
		//AndroidJavaClass jc = new AndroidJavaClass("java.lang.Object");

    }


    // Use this for initialization
    void Start()
    {
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
		calibrationOrder = udp2CppReceiver.GetComponent<UDP2CppReceive>().UDPGetPacket();
		string[] calibrationOrders = calibrationOrder.Split('/');

		if (calibrationOrders.Length > 1)
		if (currentCalibrationOrder.nbrOrder != int.Parse(calibrationOrders[0]))
		{
			currentCalibrationOrder = new CalibrationOrders(calibrationOrders);
			currentCalibrationOrder.doCalibrationOrders();
		}
	}
}
