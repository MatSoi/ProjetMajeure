using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListAnimation : MonoBehaviour {

    public GameObject AnimationButton;
    public GameObject ListAnimation;

    public void Move()
    {
        ListAnimation.GetComponent<Transform>().position = AnimationButton.transform.position;
    }
}
