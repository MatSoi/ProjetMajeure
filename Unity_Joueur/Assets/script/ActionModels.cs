using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModels : MonoBehaviour {

    public GameObject _obj = null;

    // the object to move
    private GameObject _movingObj = null;
    public Vector3 _endPos = Vector3.zero;

    public bool MOVE_MODEL = false;
    public bool _move = false;
    public float _speed = 20.0f;

    // the object to rotate
    private GameObject _rotatingObj = null;
    private Vector3 mouseOrigin;

    public bool ROTATE_MODEL = false;
    public bool _rotate = false;
    public float turnSpeed = 4.0f;      // Speed of rotation when mouse moves in along an axis

    // time to move from begin to end pos
    private float lerpTime;
    private float currentLerpTime = 0;

    Rigidbody rb;
    Transform character;

    // Use this for initialization
    void Start()
    {
        //begin_pos = character_to_move.transform.position;

        //rb = GetComponent<Rigidbody>();
        //character = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (MOVE_MODEL)
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
                    _endPos.y += _obj.GetComponent<Transform>().position.y;
                }
                if (!_move)
                {
                    _move = true;
                    _movingObj = _obj;
                }

                Invoke("Deactivate", 0.1f);
            }
        }

        if(ROTATE_MODEL)
        {
            // detecter clic souris
            if (Input.GetMouseButtonDown(0))
            {
                if (!_rotate)
                {
                    // Get mouse origin
                    mouseOrigin = Input.mousePosition;
                    _rotate = true;
                    _rotatingObj = _obj;
                }

                Invoke("Deactivate", 0.1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_movingObj && _move)
        {
            if ((_movingObj.GetComponent<Transform>().position.x == _endPos.x
                && _movingObj.GetComponent<Transform>().position.z == _endPos.z /*doesn't work all the time*/)
                /*|| _obj.GetComponent<Transform>().position == _oldPos/*|| same pos as previous*/
                                                          /*|| collision detected*/)
            {
                _move = false;
                _movingObj = null;
            }
            if (_move)
            {
                _movingObj.GetComponent<Transform>().position = Vector3.MoveTowards(_movingObj.GetComponent<Transform>().position,
                                                                                 _endPos,
                                                                                 _speed * Time.deltaTime);
            }
        }

        if(_rotatingObj && _rotate)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            _rotatingObj.transform.RotateAround(_rotatingObj.transform.position, Vector3.up, pos.x * turnSpeed);

            if (Input.GetMouseButtonUp(0))
                _rotate = false;
        }
    }

    void Deactivate()
    {
        MOVE_MODEL = false;
        ROTATE_MODEL = false;
    }

    public void MoveModel()
    {
        MOVE_MODEL = true;
    }

    public void RotateModel()
    {
        ROTATE_MODEL = true;
    }

    public void DeleteModel()
    {
        GameObject.Destroy(_obj);
    }
}
