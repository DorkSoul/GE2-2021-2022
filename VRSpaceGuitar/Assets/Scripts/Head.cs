using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        
        // Check if the forward vector is not zero
        Vector3 projectedForward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;
        if (projectedForward != Vector3.zero)
        {
            rootObject.forward = projectedForward;
        }

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
