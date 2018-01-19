using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

    public Item item;
    public int amount;
    public int slotid;

    private InventoryUI inv;
    private ToolTip tooltip;

    private EquipmentUI equip;
    public int dragplace = 0;
    public int dropplace = 0;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        tooltip = inv.GetComponent<ToolTip>();

        equip = GameObject.Find("Inventory").GetComponent<EquipmentUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            //offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false; //disables the ray interaction to be able to interact with the slots below

            //Gets the information of the old item
            if (slotid < equip.slotAmount)
            {
                if (Vector2.Distance(inv.slots[slotid].transform.position, eventData.position) < Vector2.Distance(equip.slots[slotid].transform.position, eventData.position))
                {
                    dragplace = 0; //belongs to the inventory
                    Debug.Log("Drag from Inventory");
                }
                else
                {
                    dragplace = 1; //belongs to the equipment
                    Debug.Log("Drag from Equipment");
                }
            }
        }
        //DisplayItems();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position; //follows the mouse
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotid < equip.slotAmount)
        {
            if (Vector2.Distance(inv.slots[slotid].transform.position, eventData.position) > Vector2.Distance(equip.slots[slotid].transform.position, eventData.position))
            {
                dropplace = 1;
                Debug.Log("Drop in Equipment");
                this.transform.SetParent(equip.slots[slotid].transform);
                this.transform.position = equip.slots[slotid].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        if(GetComponent<CanvasGroup>().blocksRaycasts == false) //shows that the item was not dragged
        {
            dropplace = 0;
            Debug.Log("Drop in Inventory");
            this.transform.SetParent(inv.slots[slotid].transform);
            this.transform.position = inv.slots[slotid].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        DisplayItems();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

    public void DisplayItems()
    {
        for(int i=0; i<inv.items.Count; i++)
        {
            if(inv.items[i].ID != -1)
                Debug.Log("ind" + i + " :" + inv.items[i].ID);
        }

        for (int i = 0; i < equip.items.Count; i++)
        {
            if (equip.items[i].ID != -1)
                Debug.Log("ind" + i + " :" + equip.items[i].ID);
        }
    }
}