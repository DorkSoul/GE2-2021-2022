using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private float fallSpeed = 5f;
    private const float targetYPosition = 0.0f;

    private void Update()
    {
        Vector3 newPos = transform.position;
        newPos.y -= fallSpeed * Time.deltaTime;

        if (newPos.y < targetYPosition)
        {
            newPos.y = targetYPosition;
        }
        transform.position = newPos;
    }
}