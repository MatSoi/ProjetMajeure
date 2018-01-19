using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListAnimation : MonoBehaviour {

    public GameObject AnimationButton;
    public GameObject ListAnimation;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        ListAnimation.GetComponent<Transform>().position = AnimationButton.transform.position;
    }
}
