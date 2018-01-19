using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnModels : MonoBehaviour
{
    public Vector3 PosWhereToSpawn;

    private GameObject ModelToClone;
    private Vector3 Offset;
    private GameObject Scene;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnPrefab()
    {
        string ClickedButtonName = EventSystem.current.currentSelectedGameObject.name; //recup le nom du button
        //ModelToClone = GameObject.FindGameObjectWithTag(ClickedButtonName);
        GameObject ModelToClone = (GameObject)Resources.Load("Prefab/" + ClickedButtonName, typeof(GameObject));

        if (ModelToClone.GetComponent<Collider>())
            Offset.y = ModelToClone.GetComponent<Collider>().bounds.extents.y;
        else
            Offset.y = ModelToClone.GetComponent<Transform>().position.y;

        PosWhereToSpawn = GameObject.Find("Terrain").GetComponent<ToggleSelection>().hitPos;
        PosWhereToSpawn.y = PosWhereToSpawn.y + Offset.y;
        Scene = GameObject.Find("Scene");

        GameObject instantiatedObj = Object.Instantiate<GameObject>(ModelToClone, PosWhereToSpawn, ModelToClone.GetComponent<Transform>().rotation, Scene.GetComponent<Transform>());

        // add list to toggle selection
        instantiatedObj.GetComponent<ToggleSelection>().ListSelection = GameObject.Find("Terrain").GetComponent<ToggleSelection>().ListSelection;
        instantiatedObj.GetComponent<ToggleSelection>().ListAction = GameObject.Find("Terrain").GetComponent<ToggleSelection>().ListAction;
        if(instantiatedObj.tag == "dragon" || instantiatedObj.tag == "wizard")
            instantiatedObj.GetComponent<ToggleSelection>().ListAnimation = GameObject.Find("Terrain").GetComponent<ToggleSelection>().ListAnimation;
    }
}
