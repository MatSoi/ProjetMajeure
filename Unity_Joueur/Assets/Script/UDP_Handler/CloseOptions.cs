using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptions : MonoBehaviour {
    public GameObject[] Menus;

    bool visible = false;

    public void closeOptions()
    {
        foreach (GameObject menu in Menus)
        {
            menu.SetActive(visible);
        }
        visible = !visible;
    }
}
