using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour {
    private static FloatingTextController instance;
    public GameObject textPrefab;

    public RectTransform canvasTransform;
    public Vector3 direction;
    public float speed;
    public float fadeTime;

    public GameObject player;

    public static FloatingTextController Instance
    {
        get
        {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<FloatingTextController>();
            }
            return instance;

        }
    }

    public void CreateText(Vector3 position, string text, Color color, bool crit)
    {
        GameObject sct = (GameObject) Instantiate(textPrefab, position, Quaternion.identity);
        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        sct.GetComponent<RectTransform>().position = position;

        sct.GetComponent<FloatingText>().Initialize(speed, direction, fadeTime, crit);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;

    }

}
