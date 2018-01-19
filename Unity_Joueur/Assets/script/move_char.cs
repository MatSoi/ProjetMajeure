using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_char : MonoBehaviour {

    public float _speed;
    private bool _move=false;

    // the object to move
    public GameObject _obj;
    private Vector3 _endPos;

    // time to move from begin to end pos
    private float lerpTime;
    private float currentLerpTime = 0;

    Rigidbody rb;
    Transform character;

    // Use this for initialization
    void Start () {
        //begin_pos = character_to_move.transform.position;


        rb = GetComponent<Rigidbody>();
        //character = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // detecter clic souris
        if (Input.GetButtonDown("Fire1"))
        {
            // point de collision de ma souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _endPos = hit.point;
            }
            if(!_move)
                _move = true;
            //Instantiate(_obj, _endPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update () {
        if (transform.position == _endPos)
            _move = false;
        if (_move)
            transform.position = Vector3.MoveTowards(transform.position, _endPos, _speed * Time.deltaTime);
	}
}
