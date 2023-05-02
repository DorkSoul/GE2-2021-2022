using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{   
    //variables
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    public float detectionRadius = 5.0f;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        if (targetGameObject != null)
        {
            float distance = Vector3.Distance(transform.position, target);
            if (distance <= detectionRadius)
            {
                return boid.SeekForce(target);
            }
        }
        return Vector3.zero;
    }

    public void Update()
    {   
        //if the targetGameObject is null then find the closest food with the tag "Food"
        FindClosestFoodWithTag();
    }

    //find the closest object with the tag "Food"
    private void FindClosestFoodWithTag()
    {   
        //find all the objects with the tag "Food"
        GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
        //set the closest distance to the detectionRadius
        float closestDistance = detectionRadius;
        //set the closest food to null
        GameObject closestFood = null;

        //loop through all the food objects
        foreach (GameObject food in foodObjects)
        {   
            //find the distance between the object and the food
            float distance = Vector3.Distance(transform.position, food.transform.position);
            //if the distance is less than the closest distance 
            //then set the closest distance to the distance and set the closest food to the food
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFood = food;
            }
        }

        //set the targetGameObject to the closest food
        targetGameObject = closestFood;
        //set the target to the closest food's position
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
    }
}