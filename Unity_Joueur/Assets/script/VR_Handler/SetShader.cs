using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShader : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        ChangeShader();
    }

    private void ChangeShader()
    {
        Shader barillet = Shader.Find("Custom/BarilletShader");
    	GetComponent<Camera>().SetReplacementShader(barillet, "");
    }
}
