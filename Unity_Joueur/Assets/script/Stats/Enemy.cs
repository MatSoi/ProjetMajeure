using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float speed;

    public void Awake()
    {
        /*health.MaxVal = 100;
        energy.MaxVal = 100;
        shield.MaxVal = 0;
        attack_damage.MaxVal = 20;
        armor.MaxVal = 15;*/
        //attack_damage.Initialize();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            FloatingTextController.Instance.CreateText(transform.GetChild(0).transform.position, "-60", Color.red, true);
            
        }
    }
}
