using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{   
    //variables
    private BoxCollider fenceCollider;
    public float spawnCheckInterval = 2f;
    public GameObject newCreature;
    public AudioSource Player;
    // Start is called before the first frame update
    void Start()
    {
        //get the fence collider
        fenceCollider = GetComponent<BoxCollider>();
        //start the coroutine
        StartCoroutine(CheckCreaturesInsideFence());
    }

    //OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        //if the other object is a creature
        if (other.CompareTag("Creature"))
        {
            //disable the creature's scripts
            ToggleCreatureScripts(other.gameObject, false);
        }
    }

    //OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        //if the other object is a creature
        if (other.CompareTag("Creature"))
        {
            //enable the creature's scripts
            ToggleCreatureScripts(other.gameObject, true);
        }
    }
    //OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    void OnTriggerStay(Collider other)
    {
        //if the other object is a creature
        if (other.CompareTag("Creature"))
        {
            //keep the creature upright
            KeepCreatureUpright(other.gameObject);
        }
    }

    //check if the creatures are inside the fence
    IEnumerator CheckCreaturesInsideFence()
    {
        Debug.Log("Checking for creatures inside the fence");
        //loop forever
        while (true)
        {
            //wait for spawnCheckInterval seconds
            yield return new WaitForSeconds(spawnCheckInterval);

            //get all the creatures inside the fence
            Collider[] creaturesInsideFence = Physics.OverlapBox(
                fenceCollider.bounds.center,
                fenceCollider.bounds.extents,
                Quaternion.identity,
                LayerMask.GetMask("Creature")
            );
            Debug.Log("Number of creatures inside the fence: " + creaturesInsideFence.Length/2);

            //if there are 2 creatures inside the fence
            if (creaturesInsideFence.Length == 4)
            {
                Debug.Log("Two creatures inside the fence");
                //create a new creature
                CreateMixedCreature(creaturesInsideFence[0].gameObject, creaturesInsideFence[2].gameObject);
            }

            //wait for spawnCheckInterval seconds
            float elapsedTime = 0;
            //loop while elapsedTime is less than spawnCheckInterval
            while (elapsedTime < spawnCheckInterval)
            {
                //increment elapsedTime by the amount of time that has passed since the last frame
                elapsedTime += Time.fixedDeltaTime;
                //wait for the next frame
                yield return new WaitForFixedUpdate();
            }
        }
    }

    //create a new creature from two existing creatures
    void CreateMixedCreature(GameObject creature1, GameObject creature2)
    {
        Debug.Log("Creating a mixed creature");

        // Instantiate the new creature
        GameObject mixedCreature = Instantiate(newCreature, Vector3.zero, Quaternion.identity);
        mixedCreature.name = "MixedCreature";

        mixedCreature.tag = "Creature";
        mixedCreature.layer = LayerMask.NameToLayer("Creature");

        // array of body part tags
        string[] bodyPartTags = { "Head", "Chest", "Arm", "Leg" };

        // Loop through the body part tags
        foreach (string tag in bodyPartTags)
        {   
            // Find the body part in each creature
            GameObject bodyPart1 = FindChildWithTag(creature1, tag);
            GameObject bodyPart2 = FindChildWithTag(creature2, tag);

            // Select the body part randomly
            GameObject selectedBodyPart = (Random.Range(0, 2) == 0) ? bodyPart1 : bodyPart2;

            // If the body part was found
            if (selectedBodyPart != null)
            {
                // Instantiate the body part
                GameObject newBodyPart = Instantiate(selectedBodyPart, mixedCreature.transform);
                newBodyPart.name = tag;
                //play sound
                playSound();

                // Set the material of the new body part and its children
                SetMaterialInChildren(newBodyPart, selectedBodyPart.GetComponentInChildren<Renderer>().sharedMaterial);
            }
            else
            {
                Debug.LogError($"Body part with tag {tag} not found in creatures");
            }
        }

        // Set a suitable position for the new creature
        mixedCreature.transform.position = new Vector3(
            Random.Range(fenceCollider.bounds.min.x, fenceCollider.bounds.max.x),
            3,
            Random.Range(fenceCollider.bounds.min.z, fenceCollider.bounds.max.z)
        );
    }

    // Set the material of the game object and its children
    void SetMaterialInChildren(GameObject parent, Material material)
    {   
        // Get all the renderers in the parent and its children
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
        // Loop through the renderers
        foreach (Renderer renderer in renderers)
        {
            // Set the material of the renderer
            renderer.material = material;
        }
    }

    // Find a child of a game object with a specific tag
    GameObject FindChildWithTag(GameObject parent, string tag)
    // Loop through the children of the parent
    {
        foreach (Transform child in parent.transform)
        {
            // If the child has the tag
            if (child.CompareTag(tag))
            {
                // Return the child
                return child.gameObject;
            }
        }
        // If no child with the tag was found, return null
        return null;
    }
    // Toggle the scripts of a creature
    void ToggleCreatureScripts(GameObject creature, bool isEnabled)
    {
        Wander wanderScript = creature.GetComponent<Wander>();
        Boid boidScript = creature.GetComponent<Boid>();
        Seek seekScript = creature.GetComponent<Seek>();
        ObstacleAvoidance obstacleAvoidanceScript = creature.GetComponent<ObstacleAvoidance>();

        if (wanderScript != null) wanderScript.enabled = isEnabled;
        if (boidScript != null) boidScript.enabled = isEnabled;
        if (seekScript != null) seekScript.enabled = isEnabled;
        if (obstacleAvoidanceScript != null) obstacleAvoidanceScript.enabled = isEnabled;
    }

    // Keep the creature upright
    void KeepCreatureUpright(GameObject creature)
    {
        creature.transform.rotation = Quaternion.Euler(0, creature.transform.eulerAngles.y, 0);
    }

    //play sound
    public void playSound()
    {
        Player.Play();
    }
}