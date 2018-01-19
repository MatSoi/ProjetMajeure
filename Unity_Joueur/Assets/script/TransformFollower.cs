using UnityEngine;
using System.Collections;

public class TransformFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    Transform targetpos;

    private void Start()
    {
        targetpos = target.transform;
        //targetpos.position += Vector3.up * 10;
        //targetpos.position = targetpos.TransformPoint(targetpos.position);
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(targetpos);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}