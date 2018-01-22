using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameObject Bars;
    private bool barsEnabled = true;

    private GameObject Panels;
    private bool panelsEnabled = true;

    //delete start
    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
        shield.Initialize();
        Bars = GameObject.Find("Bars");
        Panels = GameObject.Find("Panels");
    }

    private void Start()
    {
        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Item newItem, Item oldItem)
    {
        if (newItem != null)
        {
            attack_damage.AddModifier(newItem.Damagemodifier);
            armor.AddModifier(newItem.Armormodifier);


        }
        if (oldItem != null)
        {
            attack_damage.RemoveModifier(newItem.Damagemodifier);
            armor.RemoveModifier(newItem.Armormodifier);

        }
    }
    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            int random = Random.Range(10, 30);
            if (random >= 20)
            {
                TakeDamage(random, true);
            }
            else
            {
                TakeDamage(random, false);

            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float random = Random.Range(5, 20);
            if (random >= 15)
            {
                Heal(random, true);
            }
            else
            {
                Heal(random, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            energy.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            energy.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shield.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            shield.CurrentVal += 10;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {

        }

    }

    public void ToggleBars()
    {
        this.Bars.SetActive(!barsEnabled);
        barsEnabled = !barsEnabled;
    }

    public void TogglePanels()
    {
        this.Panels.SetActive(!panelsEnabled);
        panelsEnabled = !panelsEnabled;
    }
}
