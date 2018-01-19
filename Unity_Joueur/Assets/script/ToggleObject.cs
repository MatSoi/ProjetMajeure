using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleObject : MonoBehaviour {

    public GameObject obj;
    public Vector3 hitPos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            if (!IsPointerOverUIObject())
            {
                // detecter clic souris
                if (Input.GetButtonDown("Fire1"))
                {
                    // point de collision de ma souris
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        hitPos = hit.point;
                    }

                    // hide/show
                    obj.SetActive(!obj.activeSelf);
                    // move to cursor
                    obj.transform.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);
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
