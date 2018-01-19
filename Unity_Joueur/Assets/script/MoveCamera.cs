// Part of the code from https://gist.github.com/JISyed/5017805
// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    //
    // VARIABLES
    //
    Vector3 cameraOriginPos = Vector3.zero;
    Quaternion cameraOriginRot = Quaternion.identity;

    public float speed = 0.0f;          // speed of camera movement forward/backward etc
    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;       // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth
    public float zoomSpeedScroll = 0.0f;      // Speed of the camera going back and forth

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?
    private bool isZooming;     // Is the camera zooming?

    private void Start()
    {
        cameraOriginPos = transform.position;
        cameraOriginRot = transform.rotation;
    }

    //
    // UPDATE
    //

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // Get the left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isRotating = true;
            }

            // Get the right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                // Get mouse origin
                mouseOrigin = Input.mousePosition;
                isPanning = true;
            }

            zoomSpeedScroll = 1000.0f;
            speed = 1000;
        }
        else
        {
            zoomSpeedScroll = 100.0f;
            speed = 100.0f;
        }

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(scroll * zoomSpeedScroll * transform.forward, Space.World);

        // Disable movements on button release
        if (!Input.GetMouseButton(0)) isRotating = false;
        if (!Input.GetMouseButton(1)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Move the camera linearly along Z axis
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.Self);
        }
    }

    public void ReplaceCameraToOrigin()
    {
        transform.SetPositionAndRotation(cameraOriginPos, cameraOriginRot);
    }
}