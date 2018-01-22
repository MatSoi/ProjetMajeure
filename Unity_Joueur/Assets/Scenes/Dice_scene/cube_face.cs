using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_face : MonoBehaviour {

    cube_script script;

	// Use this for 'before loading'
    // could use void Start either
	void Awake () {
        script = GameObject.Find("dice6").GetComponent<cube_script>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "floor")
        {
            script.face = int.Parse(gameObject.name);
        }
    }
}
