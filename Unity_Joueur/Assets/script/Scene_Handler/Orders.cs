using UnityEngine;

public class Orders  {

    public int nbrOrder;
    public string type;
    public string nameObject;
    public Vector3 value;

    public Orders ()
    {
        nbrOrder = 0;
    }

    public Orders(string[] order)
    {
        nbrOrder   = int.Parse(order[0]);
		type = order [1];
        nameObject = order[2];
        value      = new Vector3(float.Parse(order[3]), float.Parse(order[4]), float.Parse(order[5]));
    }

    public void doOrder()
    {
        switch(type)
        {
            case "R":
                GameObject.Find(nameObject).transform.Rotate(value);
                break;
            case "T":
                GameObject.Find(nameObject).transform.Translate(value);
                break;
            case "I":
                string[] name_object = nameObject.Split('_');
                Debug.Log(name_object[0]);
                GameObject prefab = (GameObject)Resources.Load("Prefab/" + name_object[0], typeof(GameObject));
                Object.Instantiate(prefab);
                break;
            //case "DISPLAY":
            //    GameObject.Find(nameObject).SetActive(false);
            //    break;
            default:
                Debug.Log("YOLO");
                break;
        }
    }
}
