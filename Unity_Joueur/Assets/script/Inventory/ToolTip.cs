﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//generates the details displayed when hovering an item in the inventory/equipment
/**
 * Details generation class, active when hovering an item in InventoryUI or EquipmentUI
 **/
public class ToolTip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("ToolTip"); // ! \\ IMPORTANT to get the reference before deactivating
        tooltip.SetActive(false); //done after or else the find function won't work, makes the tooltip disappear
    }

    private void Update()
    {
        if(tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition; //makes the tooltip follow the mouse, offset can be added here
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);    //activates the game object tooltip
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);   //disappears when not hovering
    }

    // Creates the text content of the tooltip
    public void ConstructDataString()
    {
        //HTML <> can be used to format the text
        data = "<color=#DF7401><b>" + item.Title + "</b></color>\n" + item.Description;
        if (item.Healthmodifier > 0)
            data += " +" + item.Healthmodifier + " HP";
        if (item.Energymodifier > 0)
            data += " +" + item.Energymodifier + " MP";
        if (item.Armormodifier > 0)
            data += " +" + item.Armormodifier + " Def";
        if (item.Damagemodifier > 0)
            data += " +" + item.Damagemodifier + " Dmg";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
