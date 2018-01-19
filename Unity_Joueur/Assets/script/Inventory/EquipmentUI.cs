using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : InventoryUI {

    void Start()
    {
        database = GetComponent<ItemDataBase>(); //simple because belonging to the same object
        slotAmount = 8;
        inventoryPanel = GameObject.Find("EquipmentPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().slotid = i;
            slots[i].name = "Empty Slot";
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.SetPositionAndRotation(slotPanel.transform.position, Quaternion.identity);
        }
    }
}

public enum EquipmentType { Nowhere, Head, Chest, Legs, Feet, Weapon, Shield }
