using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayers : MonoBehaviour {

	public void DisplayPlayerStats()
    {
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.DISPLAY,
            "Player", Vector3.zero);
    }
}
