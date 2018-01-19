using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item {

    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public float Healthmodifier { get; set; }
    public float Energymodifier { get; set; }
    public float Armormodifier { get; set; }
    public float Damagemodifier { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }
    public int EquipmentSlot { get; set; }

    public Item(int id, string title, int value, float healthmodifier, float energymodifier,
        float armormodifier, float damagemodifier, string description, bool stackable, int rarity, string slug, int equipmentslot)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Healthmodifier = healthmodifier;
        this.Energymodifier = energymodifier;
        this.Armormodifier = armormodifier;
        this.Damagemodifier = damagemodifier;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
        this.EquipmentSlot = equipmentslot;
    }

    public Item()
    {
        this.ID = -1;
    }

    public Item(int id, string title, int value)
    {
        ID = id;
        Title = title;
        Value = value;
    }

    public virtual void Use()
    {
        Debug.Log("Using " + this.Title);
    }
}