using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Drag & Drop Management class, contains necessary to achieve drag and drop between InventoryUI and EquipmentUI
 **/
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
            this.transform.SetParent(this.transform.parent.parent.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false; //disables the ray interaction to be able to interact with the slots below

            //Gets the information of the old item
            if (slotid < equip.slotAmount)
            {
                if (Vector2.Distance(inv.slots[slotid].transform.position, eventData.position) < Vector2.Distance(equip.slots[slotid].transform.position, eventData.position))
                {
                    dragplace = 0; //belongs to the inventory
                }
                else
                {
                    dragplace = 1; //belongs to the equipment
                }
            }
        }
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
                this.transform.SetParent(equip.slots[slotid].transform);
                this.transform.position = equip.slots[slotid].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        if(GetComponent<CanvasGroup>().blocksRaycasts == false) //shows that the item was not dragged
        {
            this.transform.SetParent(inv.slots[slotid].transform);
            this.transform.position = inv.slots[slotid].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}