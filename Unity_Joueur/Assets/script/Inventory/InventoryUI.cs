using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Links the database to a UI
 */ 
public class InventoryUI : MonoBehaviour {

    //UI parameters
    public GameObject inventoryPanel;
    public GameObject slotPanel;

    [SerializeField]
    public GameObject inventorySlot;

    [SerializeField]
    public GameObject inventoryItem;

    public ItemDataBase database;
    public int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    // Use this for initialization
    void Start () {

        database = GetComponent<ItemDataBase>(); //simple because belonging to the same object
        slotAmount = 20;
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        for(int i=0; i<slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().slotid = i;
            slots[i].name = "Empty Slot";
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.SetPositionAndRotation(slotPanel.transform.position,Quaternion.identity);
        }

        //Do whatever for start items
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(1);
    }
	
    //Adds an item to the inventory
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id); //grabs in the database the item to add
        for(int i = 0; i< items.Count; i++)
        {
            //If not stackable, we find an empty slot
            //If stackable, we find the corresponding slot or an empty slot if there is none yet (works with -1 corresponding to empty slot)
            if( ((items[i].ID == -1) && !items[i].Stackable) || items[i].Stackable && (items[i].ID == isInInventory(id))) //if the slot is empty
            {
                if (items[i].ID == -1) //if we are on an empty slot then it is a first entry
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slotid = i;
                    itemObj.transform.SetParent(slots[i].transform, false);
                    itemObj.transform.localPosition = Vector2.zero; //put the item in the center of the slot
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    slots[i].name = "Slot for : " + itemToAdd.Title;
                    itemObj.name = itemToAdd.Title;
                }
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                data.amount++; //set the amount to 1 if this is the first item in the slot
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                break; //you have to add the item just once
            }
        }
    }

    //Check if a determined item is in the inventory
    private int isInInventory(int id)
    {
        for(int i=0; i<items.Count; i++)
        {
            if (items[i].ID == id)
                return i;
        }
        return -1;
    }
}