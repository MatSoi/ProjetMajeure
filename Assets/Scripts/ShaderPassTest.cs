using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderPassTest : MonoBehaviour {

	public Material testPassMaterial;
	public RenderTexture testPassTexture;

    //public GameObject signal;

	// Use this for initialization
	void Update () {
		
		// Perform the first pass, blending the 4 textures and creating one big dual screen texture
		Graphics.Blit(null, testPassTexture, testPassMaterial);
	}
	
	// Update is called once per frame
	void Start () {
		//Passing camera feedback to the first pass material
		GetCameraFeedback (testPassMaterial);
	}

	void GetCameraFeedback (Material firstPassMaterial) {
		if (Application.HasUserAuthorization (UserAuthorization.WebCam)) {
			WebCamDevice[] devices = WebCamTexture.devices;
            //signal.GetComponentInChildren<Text>().text = "Nbr Webcams : " + devices.Length.ToString();

            // for debugging purposes, prints available devices to the console
            for (int i = 0; i < devices.Length; i++) {
				print ("Webcam available: " + devices [i].name);
			}

			// assuming the first available WebCam is desired
			WebCamTexture tex = new WebCamTexture (devices [0].name);
			tex.wrapMode = TextureWrapMode.Repeat;

			firstPassMaterial.SetTexture ("_MainTex", tex);

			tex.Play ();
		} else
			Debug.LogError ("No permission left camera");
	}
}
