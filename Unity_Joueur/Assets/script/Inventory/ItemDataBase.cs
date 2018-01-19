using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class ItemDataBase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); //path to file not recompiled everytime
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for(int i=0; i< database.Count; i++)
        {
            if (database[i].ID == id)
                return database[i];
        }
        return null;
    }

    private void ConstructItemDatabase()
    {
        for(int i=0; i< itemData.Count; i++)
        {
            database.Add(new Item(int.Parse(itemData[i]["id"].ToString()), itemData[i]["title"].ToString(), 
                int.Parse(itemData[i]["value"].ToString()), 
                float.Parse(itemData[i]["stats"]["healthmodifier"].ToString()), 
                float.Parse(itemData[i]["stats"]["energymodifier"].ToString()),
                float.Parse(itemData[i]["stats"]["armormodifier"].ToString()), 
                float.Parse(itemData[i]["stats"]["damagemodifier"].ToString()), 
                itemData[i]["description"].ToString(), bool.Parse(itemData[i]["stackable"].ToString()),
                int.Parse(itemData[i]["rarity"].ToString()), itemData[i]["slug"].ToString(),
                int.Parse(itemData[i]["equipmentslot"].ToString())));
        }
    }
}
