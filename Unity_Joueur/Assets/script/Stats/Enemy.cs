using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float speed;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            FloatingTextController.Instance.CreateText(transform.GetChild(0).transform.position, "-60", Color.red, true);
            
        }
    }
}
