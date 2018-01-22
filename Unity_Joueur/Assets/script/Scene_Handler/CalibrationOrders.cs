using UnityEngine;

public class CalibrationOrders
{
	public string[] type;
	public string[] nameObject;
	public float[][] values;

	//Get reference to the camera to change their projection matrix later on
	private Camera leftCam;//Reference to left camera
	private Camera rightCam;//Reference to right camera

	public CalibrationOrders (string[] order)
	{
		leftCam = GameObject.Find ("LeftCameraVirtual").GetComponent<Camera>();
		rightCam = GameObject.Find ("RightCameraVirtual").GetComponent<Camera>();

		type = new string[order.Length];
		nameObject = new string[order.Length];
		values = new float[order.Length][];

		for(int i=0; i<order.Length; i++)
		{
			string[] currentOrder = order[i].Split('/');
			type[i] = currentOrder[0];//Type is the first string sent
			nameObject[i] = currentOrder[1];//Object name is the second string sent
			values[i] = new float[currentOrder.Length - 2];//The others strings corresponds to values
			for (int j = 2; j < currentOrder.Length; j++) {
				values[i][j - 2] = float.Parse (currentOrder [j]);
			}
		}
	}

	public void doCalibrationOrders ()
	{
		for (int i = 0; i < type.Length; i++) {
			switch (type[i]) {//Switch Case depending on the type of the order received
			//Treat rotation order from the calibration application
			case "R":
				GameObject.Find(nameObject[i]).transform.Rotate(new Vector3(values[i][0],values[i][1],values[i][2]));
				if (nameObject[i] == "left")
					leftCam.transform.rotation = new Quaternion (values [i][0], values [i][1], values [i][2], 1);
				if (nameObject[i] == "right")
					leftCam.transform.rotation = new Quaternion (values [i][0], values [i][1], values [i][2], 1);
				if (nameObject[i] == "zero") {	
					leftCam.transform.rotation = new Quaternion (values [i][0], values [i][1], values [i][2], 1);
					rightCam.transform.rotation = new Quaternion (values [i][0], values [i][1], values [i][2], 1);
				}
				break;
			//Treat Positioning order from the calibration application
			case "P":
				if (nameObject[i] == "left")
					leftCam.transform.position = new Vector3 (values [i][0], values [i][1], values [i][2]);
				if (nameObject[i] == "right")
					rightCam.transform.position = new Vector3 (values [i][0], values [i][1], values [i][2]);
				if (nameObject[i] == "zero") {	
					leftCam.transform.position = new Vector3 (values [i][0], values [i][1], values [i][2]);
					rightCam.transform.position = new Vector3 (values [i][0], values [i][1], values [i][2]);
				}
				break;
			//Treat projection matrix order from the calibration application
			case "F":
				if (values[i].Length >= 16) {//Force to have 16 values to construct 4x4 matrix	
					Matrix4x4 projMatrix = createMatrix4x4 (values[i]);
					if (nameObject[i] == "left") 
						leftCam.projectionMatrix = projMatrix;
					if (nameObject[i] == "right")
						rightCam.projectionMatrix = projMatrix;
					if (nameObject[i] == "zero") {	
						leftCam.projectionMatrix = projMatrix;
						rightCam.projectionMatrix = projMatrix;
					}
				}
				break;
			default:
				break;
			}
		}
	}

	/** Create 4x4 matrix from an array of values **/
	Matrix4x4 createMatrix4x4 (float[] values)
	{
		Matrix4x4 result = new Matrix4x4();
		result.SetRow (0, new Vector4 (values [0], values [1], values [2], values [3]));
		result.SetRow (1, new Vector4 (values [4], values [5], values [6], values [7]));
		result.SetRow (2, new Vector4 (values [8], values [9], values [10], values [11]));
		result.SetRow (3, new Vector4 (values [12], values [13], values [14], values [15]));

		return result;
	}
}
