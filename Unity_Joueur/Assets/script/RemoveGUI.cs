using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//allows use for Text data type

public class RemoveGUI : MonoBehaviour
{
    //instantiate changeGUI to reduce typing code
    GameObject changeGUI;

    //this code will be performed when you first open the scene
    void Start()
    {
        changeGUI = transform.parent.gameObject;//get parent object (the gui list);
    }

    //This function will be used to hide the gui
    public void GUI()
    {
        changeGUI.SetActive(false);    //hide GUI
    }
}