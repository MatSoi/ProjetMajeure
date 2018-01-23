using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//manages dropping in inventory/equipment both graphical and internally
public class Slot : MonoBehaviour, IDropHandler {
    public int slotid;
    private InventoryUI inv;

    private EquipmentUI equip;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        equip = GameObject.Find("Inventory").GetComponent<EquipmentUI>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        Transform item = null;

        //Test to determine dropplace (modified if equipment)
        if(slotid < equip.slotAmount)
            if (Vector2.Distance(inv.slots[slotid].transform.position, eventData.position) > Vector2.Distance(equip.slots[slotid].transform.position, eventData.position))
                droppedItem.dropplace = 1;

        //Effect on dropplace
        if (droppedItem.dropplace == 0) //ends in inventory
        {
            if (inv.items[slotid].ID != -1)//there is an item
            {
                item = this.transform.GetChild(0); //grab the item in the slot
                item.GetComponent<ItemData>().slotid = droppedItem.slotid;
                item.transform.SetParent(inv.slots[droppedItem.slotid].transform); //grabs corresponding transforms
                item.transform.position = inv.slots[droppedItem.slotid].transform.position;
            }
            inv.items[slotid] = droppedItem.item; //fill the new slot
            inv.slots[slotid].name = "Slot for : " + droppedItem.item.Title;
        }
        else //ends in equipment
        {
            if(slotid < equip.slotAmount) //verification of the bounds
            {
                if (equip.items[slotid].ID != -1)//there is an item
                {
                    item = this.transform.GetChild(0); //grab the item in the slot
                    item.GetComponent<ItemData>().slotid = droppedItem.slotid;
                    item.transform.SetParent(equip.slots[droppedItem.slotid].transform);
                    item.transform.position = equip.slots[droppedItem.slotid].transform.position;
                }
                equip.items[slotid] = droppedItem.item; //fill the new slot
                equip.slots[slotid].name = "Slot for : " + droppedItem.item.Title;
            }
        }

        //Effect on dragplace
        if(droppedItem.dragplace == 0) //dragged from inventory
        {
            if (item == null) //There was no item in the dropplace
            {
                inv.items[droppedItem.slotid] = new Item(); //empty the old slot
                inv.slots[droppedItem.slotid].name = "Empty Slot";
            }
            else
            {
                inv.items[droppedItem.slotid] = item.GetComponent<ItemData>().item;
                inv.slots[droppedItem.slotid].name = "Slot for : " + item.GetComponent<ItemData>().item.Title;
            }
        }
        else //dragged from equipment
        {
            if (droppedItem.slotid < equip.slotAmount)
            {
                if (item == null) //There was no item in the dropplace
                {
                    equip.items[droppedItem.slotid] = new Item(); //empty the old slot
                    equip.slots[droppedItem.slotid].name = "Empty Slot";
                }
                else
                {
                    equip.items[droppedItem.slotid] = item.GetComponent<ItemData>().item;
                    equip.slots[droppedItem.slotid].name = "Slot for : " + item.GetComponent<ItemData>().item.Title;
                }
            }
        }

        droppedItem.slotid = slotid; //replaces the slotid of droppeditem
    }
}
