using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    //variables
    private float fallSpeed = 5f;
    private const float targetYPosition = 0.0f;

    // Update is called once per frame
    private void Update()
    {
        //newPos is the current position of the object
        Vector3 newPos = transform.position;
        //fallSpeed is the speed at which the object falls
        newPos.y -= fallSpeed * Time.deltaTime;

        //if the object is below the targetYPosition then set the object's position to the targetYPosition
        if (newPos.y < targetYPosition)
        {
            newPos.y = targetYPosition;
        }
        transform.position = newPos;
    }
}