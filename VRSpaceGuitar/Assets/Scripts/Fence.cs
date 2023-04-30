using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private BoxCollider fenceCollider;
    public float spawnCheckInterval = 2f;
    public GameObject newCreature;

    // Start is called before the first frame update
    void Start()
    {
        fenceCollider = GetComponent<BoxCollider>();
        StartCoroutine(CheckCreaturesInsideFence());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            ToggleCreatureScripts(other.gameObject, false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            ToggleCreatureScripts(other.gameObject, true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            KeepCreatureUpright(other.gameObject);
        }
    }

    IEnumerator CheckCreaturesInsideFence()
    {
        Debug.Log("Checking for creatures inside the fence");
        while (true)
        {
            yield return new WaitForSeconds(spawnCheckInterval);

            Collider[] creaturesInsideFence = Physics.OverlapBox(
                fenceCollider.bounds.center,
                fenceCollider.bounds.extents,
                Quaternion.identity,
                LayerMask.GetMask("Creature")
            );
            Debug.Log("Number of creatures inside the fence: " + creaturesInsideFence.Length);

            if (creaturesInsideFence.Length == 2)
            {
                Debug.Log("Two creatures inside the fence");
                CreateMixedCreature(creaturesInsideFence[0].gameObject, creaturesInsideFence[1].gameObject);
            }

            float elapsedTime = 0;
            while (elapsedTime < spawnCheckInterval)
            {
                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    void CreateMixedCreature(GameObject creature1, GameObject creature2)
    {
        Debug.Log("Creating a mixed creature");

        // Instantiate the newCreature prefab
        GameObject mixedCreature = Instantiate(newCreature, Vector3.zero, Quaternion.identity);
        mixedCreature.name = "MixedCreature";

        mixedCreature.tag = "Creature";
        mixedCreature.layer = LayerMask.NameToLayer("Creature");

        string[] bodyPartTags = { "Head", "Chest", "Arm", "Leg" };
        int i = 0;

        foreach (string tag in bodyPartTags)
        {
            GameObject bodyPart1 = FindChildWithTag(creature1, tag);
            GameObject bodyPart2 = FindChildWithTag(creature2, tag);

            GameObject selectedBodyPart = (i % 2 == 0) ? bodyPart1 : bodyPart2;

            if (selectedBodyPart != null)
            {
                GameObject newBodyPart = Instantiate(selectedBodyPart, mixedCreature.transform);
                newBodyPart.name = tag;
            }
            else
            {
                Debug.LogError($"Body part with tag {tag} not found in creatures");
            }

            i++;
        }

        // Set a suitable position for the new creature
        mixedCreature.transform.position = new Vector3(
            Random.Range(fenceCollider.bounds.min.x, fenceCollider.bounds.max.x),
            0,
            Random.Range(fenceCollider.bounds.min.z, fenceCollider.bounds.max.z)
        );
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }
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

    void KeepCreatureUpright(GameObject creature)
    {
        creature.transform.rotation = Quaternion.Euler(0, creature.transform.eulerAngles.y, 0);
    }
}