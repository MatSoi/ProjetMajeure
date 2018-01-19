using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleSelection : MonoBehaviour
{

    public GameObject ListSelection;
    public GameObject ListAction;
    public GameObject ListAnimation = null;
    public GameObject hitObj;
    public Vector3 hitPos;
    
    // Use this for initialization
    void Start()
    {
        if (!ListAnimation)
            ListAnimation = ListAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        // don't show the spawn list selection when :
        if (!Input.GetKey(KeyCode.LeftAlt) //alt to move camera
            && !IsPointerOverUIObject() // over the GUI
            && !GameObject.Find("Terrain").GetComponent<ActionModels>().MOVE_MODEL // we're moving the object
            && !GameObject.Find("Terrain").GetComponent<ActionModels>().ROTATE_MODEL) // we're moving the object
        {
            // hide lists 
            if (ListAction.activeSelf || ListSelection.activeSelf || ListAnimation.activeSelf)
            {
                ListAction.SetActive(false);
                ListSelection.SetActive(false);
                ListAnimation.SetActive(false);
                return;
            }

            // detecter clic souris
            if (Input.GetButtonDown("Fire1"))
            {
                // point de collision de ma souris
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    hitPos = hit.point;

                    hitObj = this.gameObject; //  hit.transform.gameObject;
                    // send obj to all models in scene into the script ActionModels
                    if (hitObj)
                    {
                        foreach (Transform child in GameObject.Find("Scene").transform)
                        {
                            child.gameObject.GetComponent<ActionModels>()._obj = hitObj;
                        }
                    }
                }

                if (hitObj.name == "Terrain")
                {
                    // hide/show
                    ListSelection.SetActive(!ListSelection.activeSelf);
                    // move to cursor
                    ListSelection.transform.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);
                }
                else
                {
                    // hide/show
                    ListAction.SetActive(!ListAction.activeSelf);
                    // Do not show anim if the object doesn't support it (i.e. if ListAnimation == ListAction)
                    ListAction.transform.Find("Animation").gameObject.SetActive(ListAnimation != ListAction);

                    // move to cursor
                    ListAction.transform.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
