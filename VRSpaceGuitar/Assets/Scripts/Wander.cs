using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : SteeringBehaviour
{
    //variable
    public string foodTag = "Food";
    public float circleDistance = 20f;
    public float circleRadius = 10f;
    public float wanderJitter = 1f;
    public float wanderForce = 1f;

    public AudioSource Player;
    private Vector3 wanderTarget;
    public Seek seek;
    public ObstacleAvoidance obstacleAvoidance;
    private Dictionary<string, List<GameObject>> collectedBodyParts = new Dictionary<string, List<GameObject>>();
    void Start()
    {
        // Set the initial target to a random position within a circle
        wanderTarget = Random.insideUnitSphere * circleRadius;
        //get seek and obstacle avoidance
        seek = GetComponent<Seek>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();
    }

    //calculate the force
    public override Vector3 Calculate()
    {   
        // Reset the force
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
            // Calculate wander force
            Vector3 circleCenter = boid.velocity.normalized * circleDistance;
            // Restrict the Y axis
            circleCenter.y = 0; 

            // Calculate the displacement force
            wanderTarget += new Vector3(
                Random.Range(-1f, 1f) * wanderJitter,
                0,
                Random.Range(-1f, 1f) * wanderJitter);

            // Normalize wanderTarget
            wanderTarget.Normalize();
            // Multiply wanderTarget by the circle radius
            wanderTarget *= circleRadius;

            // Constrain wanderTarget within the desired range (-20 to 20)
            wanderTarget.x = Mathf.Clamp(wanderTarget.x, -20, 20);
            wanderTarget.z = Mathf.Clamp(wanderTarget.z, -20, 20);

            // Calculate the force
            force = circleCenter + wanderTarget;
            force *= this.wanderForce;
        }

        // Apply the avoidance force
        force += avoidanceForce;

        return force;
    }

    // on trigger enter
    public void OnTriggerEnter(Collider other)
    {
        // If the other object has the Food tag
        if (other.gameObject.CompareTag("Food"))
        {
            // Get the Food script from the other object
            Food foodScript = other.gameObject.GetComponent<Food>();
            // If the Food script exists
            if (foodScript != null)
            {
                // Get the body parts from the Food script
                Dictionary<string, List<int>> bodyParts = foodScript.GetBodyParts();

                // Select a random body part type (Legs, Head, Arms, or Chest)
                List<string> bodyPartTypes = new List<string>(bodyParts.Keys);
                int randomBodyPartTypeIndex = Random.Range(0, bodyPartTypes.Count - 1);
                string randomBodyPartType = bodyPartTypes[randomBodyPartTypeIndex];

                List<GameObject> prefabs = foodScript.GetBodyPartPrefabs(randomBodyPartType);

                // Select a random prefab from the prefabs list
                int randomIndex = Random.Range(0, prefabs.Count);
                GameObject randomPrefab = prefabs[randomIndex];

                // Eat the food
                EatFood(randomBodyPartType, randomPrefab);

                Destroy(other.gameObject);
            }
        }
    }

    //eat food
    void EatFood(string bodyPart, GameObject prefab)
    {
        // If the collectedBodyParts dictionary doesn't contain the body part type
        if (!collectedBodyParts.ContainsKey(bodyPart))
        {
            // Add the body part type to the dictionary
            collectedBodyParts.Add(bodyPart, new List<GameObject>());
        }

        // Add the body part to the list of collected body parts
        collectedBodyParts[bodyPart].Add(prefab);
        
        // If the number of collected body parts of this type is 4
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

    //attach body part
    void AttachBodyPart(string bodyPart, GameObject prefab)
    {
        // Find the existing body part with the same tag
        Transform existingBodyPart = null;
        // Loop through all the children of this game object
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            // If the child has the same tag as the body part
            if (child.CompareTag(bodyPart))
            {
                // Store the child as the existing body part
                existingBodyPart = child;
                break;
            }
        }

        // If an existing body part was found
        if (existingBodyPart != null)
        {
            // Store the position, rotation, and parent of the existing body part
            Vector3 existingBodyPartPosition = existingBodyPart.position;
            Quaternion existingBodyPartRotation = existingBodyPart.rotation;
            Transform existingBodyPartParent = existingBodyPart.parent;

            // Get the material of the existing body part
            Material existingBodyPartMaterial = existingBodyPart.GetComponentInChildren<Renderer>().sharedMaterial;

            // Destroy the existing body part
            Destroy(existingBodyPart.gameObject);

            // Instantiate the new body part at the position and rotation of the old body part
            GameObject newBodyPart = Instantiate(prefab, existingBodyPartPosition, existingBodyPartRotation);

            // Set the material of the new body part and its children
            SetMaterialInChildren(newBodyPart, existingBodyPartMaterial);

            // Set the new body part's parent to be the same as the old body part
            newBodyPart.transform.SetParent(existingBodyPartParent);

            // Move the new body part so that it just touches the other parts, except for the "Arm" tag
            if (!bodyPart.Equals("Arm"))
            {
                Collider newBodyPartCollider = newBodyPart.GetComponent<Collider>();
                if (newBodyPartCollider != null)
                {
                    Bounds newBodyPartBounds = newBodyPartCollider.bounds;
                    float offsetY = newBodyPartBounds.extents.y;

                    // Get the chest object
                    GameObject chest = GameObject.FindGameObjectWithTag("Chest");
                    if (chest != null)
                    {
                        Collider chestCollider = chest.GetComponent<Collider>();
                        if (chestCollider != null)
                        {
                            Bounds chestBounds = chestCollider.bounds;
                            float chestOffsetY = chestBounds.extents.y;

                            // Calculate the new position for the new body part, making sure it's just touching the chest
                            Vector3 newPosition = newBodyPart.transform.position;
                            newPosition.y = chest.transform.position.y + chestOffsetY + offsetY;
                            newBodyPart.transform.position = newPosition;
                        }
                    }
                }
            }

            // play sound if head
            if (newBodyPart.tag == "Head")
            {
                Player = newBodyPart.GetComponent<AudioSource>();
                playSound();
            }
        }
    }
    //set material
    void SetMaterialInChildren(GameObject parent, Material material)
    {
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }
    //play sound
    public void playSound()
    {
        Player.Play();
    }
}