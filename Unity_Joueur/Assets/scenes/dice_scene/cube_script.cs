using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cube_script : MonoBehaviour {
    public int face;
    Rigidbody rb;
    Transform dice;
    bool playdice = false;
    public Text txt;

    // 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dice = GetComponent<Transform>();
    }
    void FixedUpdate () {
		// detecter clic souris
        if(Input.GetButtonDown("Fire1") && rb.velocity.magnitude==0)
        {
            // point de collision de ma souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                rb.AddForce((hit.point-dice.position) * Vector3.Distance(hit.point, dice.position));
                playdice = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(playdice && rb.velocity.magnitude==0)
        {
            playdice = false;
            StartCoroutine(ShowFace());
        }
	}

    // display dice score
    IEnumerator ShowFace()
    {
        txt.enabled = true;
        txt.GetComponent<Text>().text = face.ToString();
        yield return new WaitForSeconds(2f);
        txt.enabled = false;
    }
}
