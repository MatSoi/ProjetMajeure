using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnModels : MonoBehaviour
{
    public GameObject instantiatedObj;
    public GameObject Terrain;

    private int indexInstance = 0;


    public void SpawnPrefab()
    {
        string ClickedButtonName = EventSystem.current.currentSelectedGameObject.name; //recup le nom du button
        
        GameObject ModelToClone = (GameObject)Resources.Load("Prefab/" + ClickedButtonName, typeof(GameObject));

        // position of the prefab to be on the floor (offset)
        float offsetY = ModelToClone.GetComponent<Transform>().position.y;

        Vector3 PosWhereToSpawn = Terrain.GetComponent<ToggleSelection>().hitPos;
        PosWhereToSpawn.y = PosWhereToSpawn.y + offsetY;

        instantiatedObj = Object.Instantiate<GameObject>(
            ModelToClone,
            PosWhereToSpawn,
            ModelToClone.GetComponent<Transform>().rotation,
            Terrain.GetComponentInParent<Transform>());

        // Rename object : tag /*or prefab name*/ + indexInstance
        instantiatedObj.name = instantiatedObj.tag /*ClickedButtonName*/ + indexInstance.ToString();
        indexInstance++;

        // add list to toggle selection
        instantiatedObj.GetComponent<ToggleSelection>().ListSelection = Terrain.GetComponent<ToggleSelection>().ListSelection;
        instantiatedObj.GetComponent<ToggleSelection>().ListAction = Terrain.GetComponent<ToggleSelection>().ListAction;
        if(instantiatedObj.tag == "dragon" || instantiatedObj.tag == "wizard")
            instantiatedObj.GetComponent<ToggleSelection>().ListAnimation = Terrain.GetComponent<ToggleSelection>().ListAnimation;

        // send string
        CreateString sendString = GameObject.Find("ScriptHolderCreateString").GetComponent<CreateString>();
        sendString.Create(OrderType.INSTANTIATE,
            instantiatedObj.name,
            instantiatedObj.GetComponent<Transform>().position);
    }
}
