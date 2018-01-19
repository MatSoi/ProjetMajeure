using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Scene : MonoBehaviour
{

    public GameObject udpReceiver;

    [SerializeField]
    private string path = "Assets/Inputs/calibrationOutput.txt";
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

    private Vector3 leftCameraPos;
    private Vector3 rightCameraPos;
    private Vector3 markerBoardPos;
    private List<Vector3> markerPlayerPos;

    private string order;
    Orders currentOrder = new Orders();

    public Scene()
    {
        markerPlayerPos = new List<Vector3>();
    }


    // Use this for initialization
    void Start()
    {
        readCalibrationData(path);
        //setScenePositions();

        Debug.Log(ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (udpReceiver.GetComponent<UDPReceive>().gameState == GameStates.GamePhase)
            treatOrder();
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

    void setScenePositions()
    {
        rCamera.transform.position = leftCameraPos;
        lCamera.transform.position = rightCameraPos;
        board.transform.position = markerBoardPos;
        if (markerPlayerPos.Count > 0)
            player1.transform.position = markerPlayerPos[0];
        if (markerPlayerPos.Count > 1)
            player2.transform.position = markerPlayerPos[1];
        if (markerPlayerPos.Count > 2)
            player3.transform.position = markerPlayerPos[2];
    }

    bool readCalibrationData(string path)
    {
        try
        {
            string line;
            // Create streamreader on our file
            StreamReader reader = new StreamReader(path);
            //Garbage collector is not handle basicly for StreamReader since it implements IDisposable interface
            //using enables to collect all the garbage resulting of the stream reader
            using (reader)
            {
                do
                {
                    line = reader.ReadLine();//Read current line

                    if (line != null)//If line not null
                    {
                        string[] entries = line.Split(',');//Get all entries separating line by ','
                        if (entries.Length > 3)//If more than 4 entry : name of the component + 3 coordinates
                        {
                            //Parse string to float
                            float x = float.Parse(entries[1],
                                System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                            float y = float.Parse(entries[2],
                                System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                            float z = float.Parse(entries[3],
                                System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                            //Save date according to the first entry
                            if (entries[0] == "leftCamera")
                                leftCameraPos = new Vector3(x, y, z);
                            if (entries[0] == "rightCamera")
                                rightCameraPos = new Vector3(x, y, z);
                            if (entries[0] == "markerBoard")
                                markerBoardPos = new Vector3(x, y, z);
                            if (entries[0] == "markerPlayer")
                                markerPlayerPos.Add(new Vector3(x, y, z));
                        }
                    }
                }
                while (line != null);//While not at the end of the text file
                reader.Close();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }

    public override string ToString()
    {
        string lCString = "leftCamera: " + leftCameraPos.x + ", " + leftCameraPos.y + ", " + leftCameraPos.z + "\n";
        string rCString = "rightCamera: " + rightCameraPos.x + ", " + rightCameraPos.y + ", " + rightCameraPos.z + "\n";
        string mBString = "markerBoard: " + markerBoardPos.x + ", " + markerBoardPos.y + ", " + markerBoardPos.z + "\n";
        string mP1String = "";
        string mP2String = "";
        string mP3String = "";

        if (markerPlayerPos.Count > 0)
            mP1String = "player: " + markerPlayerPos[0].x + ", " + markerPlayerPos[0].y + ", " + markerPlayerPos[0].z + "\n";
        if (markerPlayerPos.Count > 1)
            mP2String = "player: " + markerPlayerPos[1].x + ", " + markerPlayerPos[1].y + ", " + markerPlayerPos[1].z + "\n";
        if (markerPlayerPos.Count > 2)
            mP3String = "player: " + markerPlayerPos[2].x + ", " + markerPlayerPos[2].y + ", " + markerPlayerPos[2].z + "\n";

        return lCString + rCString + mBString + mP1String + mP2String + mP3String;
    }
}
