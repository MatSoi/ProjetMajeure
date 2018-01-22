using UnityEngine;

public class CalibrationOrders
{

	public int nbrOrder;
	public string type;
	public string nameObject;
	public float[] values;

	//Get reference to the camera to change their projection matrix later on
	public Camera leftCam;
	public Camera rightCam;

	public CalibrationOrders ()
	{
		nbrOrder = 0;
	}

	public CalibrationOrders (string[] order)
	{
		nbrOrder = int.Parse (order [0]);
		type = order [1];
		nameObject = order [2];
		for (int i = 3; i < order.Length; i++)
			values [i - 3] = float.Parse (order [i]);
	}

	public void doCalibrationOrders ()
	{
		switch (type) {
		//Meaning rotation matrix for a camera
		case "R":
			GameObject.Find(nameObject).transform.Rotate(new Vector3(values[0],values[1],values[2]));
			break;
		//Meaning translation matrix for a camera
		case "T":
			break;
		//Meaning position matrix for an object
		case "P":
			break;
		//Meaning projection matrix for a camera
		case "Proj":
			if (values.Length >= 16) {//Force to have 16 values to construct 4x4 matrix	
				Matrix4x4 projMatrix = createMatrix (values);
				if(nameObject == "left")
					leftCam.projectionMatrix = projMatrix;
				if(nameObject == "right")
					rightCam.projectionMatrix = projMatrix;
			}
			break;
		default:
			break;
		}
	}

	Matrix4x4 createMatrix (float[] values)
	{
		Matrix4x4 result = Matrix4x4.identity;
		result.SetRow (0, new Vector4 (values [0], values [1], values [2], values [3]));
		result.SetRow (1, new Vector4 (values [4], values [5], values [6], values [7]));
		result.SetRow (2, new Vector4 (values [8], values [9], values [10], values [11]));
		result.SetRow (3, new Vector4 (values [12], values [13], values [14], values [15]));

		return result;
	}
}
