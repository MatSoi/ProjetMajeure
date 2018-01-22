﻿using System.Collections.Generic;
using UnityEngine;

public class CreateString : MonoBehaviour {

    public GameObject UDPSender;

    public enum OrderType {
        ROTATE,
        TRANSLATE,

        INSTANTIATE,
        DELETE,

        ATTACK,
        GETHIT,
        DIE,
        IDLE1
    };

    public List<string> Buffer;
    public string currentString;
    public int nbrOrder = 1;
    
	// Update is called once per frame
	void Update () {
        if (Buffer.Count > 0)
            Send();
	}

    public void Create(OrderType order, string objName, string values = "")
    {
        //! order : nbrOrder/TypeOfAction/ObjectIndex(except when instantiate =object.name)/values
        // values can be:
        // Instantiate : vec3 pos
        // Rotate : vec3 rot
        // Translate : vec3 pos
        currentString = nbrOrder.ToString() + "/"; // numero de la commande
        currentString += order.ToString() + "/"; // ordre

        // send name of object to instantiate/rotate/delete/...
        currentString += objName + "/";
        
        currentString += values; // valeurs

        // load buffer
        Buffer.Add(currentString);

        // incrémenter cmd number
        nbrOrder++;
    }

    public void Send()
    {
        UDPSender.GetComponent<UDPSend>().sendString(Buffer[0]);
        Buffer.RemoveAt(0);
    }
}
