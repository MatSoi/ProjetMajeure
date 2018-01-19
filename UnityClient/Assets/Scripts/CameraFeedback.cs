using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFeedback : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		if (Application.HasUserAuthorization(UserAuthorization.WebCam))
		{//Check for camera permission
			WebCamDevice[] devices = WebCamTexture.devices;

			// for debugging purposes, prints available devices to the console
			for (int i = 0; i < devices.Length; i++)
			{
				print("Webcam available: " + devices[i].name);
			}

			// assuming the first available WebCam is desired
			WebCamTexture tex = new WebCamTexture(devices[0].name);
			tex.wrapMode = TextureWrapMode.Repeat;

			GetComponent<Renderer> ().material.mainTexture = tex;

			tex.Play();
		}
		else
			Debug.LogError("No permission right Camera");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
