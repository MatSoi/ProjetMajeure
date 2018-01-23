using UnityEngine;

public class Orders  {

    public int nbrOrder;
    private string type;
    private string nameObject;
    private Vector3 value;

    public Orders ()
    {
        nbrOrder = 0;
    }

    public Orders(string[] order)
    {
        nbrOrder   = int.Parse(order[0]);
		type       = order [1];
        nameObject = order[2];
        value      = new Vector3(float.Parse(order[3]), float.Parse(order[4]), float.Parse(order[5]));
    }

    public void doOrder()
    {
        switch(type)
        {
            case "ROTATE":
                GameObject.Find(nameObject).transform.eulerAngles = value;
                break;
            case "TRANSLATE":
                GameObject.Find(nameObject).transform.position = value;
                break;
            case "INSTANTIATE":
                string[] name_object = nameObject.Split('_');
                Debug.Log(name_object[0]);
                GameObject prefab = (GameObject)Resources.Load("Prefab/" + name_object[0], typeof(GameObject));
                GameObject myobject = Object.Instantiate(prefab);
                myobject.transform.parent = GameObject.Find("Scene").transform;
                myobject.transform.position = value;
                myobject.name = nameObject;
                break;
            case "DISPLAY":
                Player p = GameObject.Find(nameObject).GetComponent<Player>();
                if(value == Vector3.zero)
                    p.ToggleBars();
                if (value == Vector3.one)
                    p.TogglePanels();
                break;
            case "DELETE":
                Object.Destroy(GameObject.Find(nameObject));
                break;
            default:
                break;
        }
    }
}
