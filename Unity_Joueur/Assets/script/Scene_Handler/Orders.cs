using UnityEngine;

/**
 * Orders management class, contains behaviour for each order case
 **/
public class Orders  {

    public int nbrOrder;        //the indice of the current treated order
    private string type;        //determines the order to be done
    private string nameObject;  //contains the name of the affected entity
    private Vector3 value;      //contains additionnal information for specific orders

    public Orders ()
    {
        nbrOrder = -1;
    }

    //Initialize from a received order by UDP
    public Orders(string[] order)
    {
        nbrOrder   = int.Parse(order[0]);
		type       = order[1];
        nameObject = order[2];
        value      = new Vector3(float.Parse(order[3]), float.Parse(order[4]), float.Parse(order[5]));
    }

    //Execute the order (66) regarding it's type and informations contained in 'value'
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
            case "ATTACK":
                Debug.Log(nameObject);
                GameObject attacker = GameObject.Find(nameObject);
                GameObject.Find("Terrain").GetComponent<ActionModels>()._obj = attacker;
                attacker.GetComponent<playAnimation>().BasicAttack();
                break;
            case "HUGEATTACK":
                Debug.Log(nameObject);
                GameObject hattacked = GameObject.Find(nameObject);
                GameObject.Find("Terrain").GetComponent<ActionModels>()._obj = hattacked;
                hattacked.GetComponent<playAnimation>().FlameAttack();
                break;
            case "GETHIT":
                Debug.Log(nameObject);
                GameObject attacked = GameObject.Find(nameObject);
                GameObject.Find("Terrain").GetComponent<ActionModels>()._obj = attacked;
                attacked.GetComponent<playAnimation>().GetHit();
                break;
            case "DIE":
                Debug.Log(nameObject);
                GameObject dying = GameObject.Find(nameObject);
                GameObject.Find("Terrain").GetComponent<ActionModels>()._obj = dying;
                dying.GetComponent<playAnimation>().Die();
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
