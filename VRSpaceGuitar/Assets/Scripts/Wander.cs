using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour
{
    public string foodTag = "Food";
    public float circleDistance = 20f;
    public float circleRadius = 10f;
    public float wanderJitter = 1f;
    public float wanderForce = 1f;

    private Vector3 wanderTarget;
    public Seek seek;
    public ObstacleAvoidance obstacleAvoidance;
    private Dictionary<string, List<GameObject>> collectedBodyParts = new Dictionary<string, List<GameObject>>();
    public Transform bodyPartContainer; // The container for attached body parts

    void Start()
    {
        wanderTarget = Random.insideUnitSphere * circleRadius;
        seek = GetComponent<Seek>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();
    }

    public override Vector3 Calculate()
    {
        Vector3 force = Vector3.zero;

        // Calculate obstacle avoidance force
        Vector3 avoidanceForce = obstacleAvoidance.Calculate();

        // If the Seek script is active and enabled, and there's a food target
        if (seek.isActiveAndEnabled && seek.targetGameObject != null)
        {
            // Use the Seek script to calculate the steering force towards the food
            force += seek.Calculate() * seek.weight;
        }
        else
        {
            // Calculate wander force as before
            Vector3 circleCenter = boid.velocity.normalized * circleDistance;
            circleCenter.y = 0; // Restrict the Y axis

            wanderTarget += new Vector3(
                Random.Range(-1f, 1f) * wanderJitter,
                0, // Restrict the Y axis
                Random.Range(-1f, 1f) * wanderJitter);

            wanderTarget.Normalize();
            wanderTarget *= circleRadius;

            // Constrain wanderTarget within the desired range (-20 to 20)
            wanderTarget.x = Mathf.Clamp(wanderTarget.x, -20, 20);
            wanderTarget.z = Mathf.Clamp(wanderTarget.z, -20, 20);

            force = circleCenter + wanderTarget;
            force *= this.wanderForce;
        }

        // Apply the avoidance force
        force += avoidanceForce;

        return force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Food foodScript = other.gameObject.GetComponent<Food>();
            if (foodScript != null)
            {
                Dictionary<string, List<int>> bodyParts = foodScript.GetBodyParts();

                // Select a random body part type (Legs, Head, Arms, or Chest)
                List<string> bodyPartTypes = new List<string>(bodyParts.Keys);
                int randomBodyPartTypeIndex = Random.Range(0, bodyPartTypes.Count);
                string randomBodyPartType = bodyPartTypes[randomBodyPartTypeIndex];

                List<GameObject> prefabs = foodScript.GetBodyPartPrefabs(randomBodyPartType);

                // Select a random prefab from the prefabs list
                int randomIndex = Random.Range(0, prefabs.Count);
                GameObject randomPrefab = prefabs[randomIndex];

                EatFood(randomBodyPartType, randomPrefab);

                Destroy(other.gameObject);
            }
        }
    }

    void EatFood(string bodyPart, GameObject prefab)
    {
        if (!collectedBodyParts.ContainsKey(bodyPart))
        {
            collectedBodyParts.Add(bodyPart, new List<GameObject>());
        }

        collectedBodyParts[bodyPart].Add(prefab);

        if (collectedBodyParts[bodyPart].Count >= 4)
        {
            // Output the name of the body part being attached
            Debug.Log("4 " + bodyPart + "s collected. Adding " + prefab.name + " to the creature.");

            // Attach the body part to the creature
            AttachBodyPart(bodyPart, prefab);

            // Empty the collected body parts list for this body part type
            collectedBodyParts[bodyPart].Clear();
        }
    }

    void AttachBodyPart(string bodyPart, GameObject prefab)
    {
        // Instantiate the body part
        GameObject newBodyPart = Instantiate(prefab, bodyPartContainer);

        // Adjust the position and rotation of the body part
        newBodyPart.transform.localPosition = Vector3.zero;
        newBodyPart.transform.localRotation = Quaternion.identity;

        // Add any additional logic to connect the body part to the creature
    }
}