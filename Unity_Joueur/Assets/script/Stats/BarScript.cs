using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    //[SerializeField] //Rend visible dans l'inspector
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColors;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':'); //stores name of value
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Start ()
    {
        if(lerpColors)
        {
            content.color = fullColor;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount) //update only when changed
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
        if (lerpColors)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        //Convertir une valeur value comprise entre inMin et inMax 
        //vers une valeur proportionelle dans outMin, outMax
        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }
}
