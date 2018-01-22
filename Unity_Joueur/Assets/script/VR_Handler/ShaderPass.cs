using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderPass : MonoBehaviour {

	public Material blendPassMaterial;
	public RenderTexture blendPassTexture;

	public Material barilletMaterial;
	public RenderTexture barilletTexture;

	// Use this for initialization
	void Update () {
		
		// Perform the first pass, blending the 4 textures and creating one big dual screen texture
		Graphics.Blit(null, blendPassTexture, blendPassMaterial);

		//Perform the second pass, to get barrillet output and cut the indesirable zone.
		Graphics.Blit(blendPassTexture, barilletTexture, barilletMaterial);
	}
	
	// Update is called once per frame
	void Start () {
		//Passing camera feedback to the first pass material
		GetCameraFeedback (blendPassMaterial);
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

			//WebCamTexture tex2 = new WebCamTexture (devices [1].name);
			//tex2.wrapMode = TextureWrapMode.Repeat;

			firstPassMaterial.SetTexture ("_MainTexLeft", tex);
            firstPassMaterial.SetTexture("_MainTexRight", tex);

            tex.Play ();
			//tex2.Play ();
		} else
			Debug.LogError ("No permission left camera");
	}
}
