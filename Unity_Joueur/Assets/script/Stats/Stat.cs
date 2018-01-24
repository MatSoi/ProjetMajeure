using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    // delete start and update and monobehaviour
    [SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;

    private List<float> modifiers = new List<float>();

    public float CurrentVal
    {
        get
        {
            float finalVal = currentVal;
            modifiers.ForEach(x => finalVal += x);
            return finalVal;
        }

        set
        {
            currentVal = Mathf.Clamp(value,0,MaxVal); //limits value between 0 and MaxVal
            bar.Value = currentVal;
        }
    }

    /**
     * Define the Max Value of the stat (getter and setter)
     **/
    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
    

    /*
     * 
     */
    public void AddModifier(float modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
