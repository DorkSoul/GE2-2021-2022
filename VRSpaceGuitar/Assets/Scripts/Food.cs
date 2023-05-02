using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // body parts
    public GameObject[] LegsPrefab;
    public GameObject[] HeadPrefab;
    public GameObject[] ArmsPrefab;
    public GameObject[] ChestPrefab;

    // variables
    public float hoverHeight = 2f;
    public float rotationSpeed = 10f;
    public float hoverAmplitude = 0.5f;
    public float hoverFrequency = 1f;
    private float initialXPosition;
    private float initialZPosition;
    private string foodType;
    private Dictionary<string, List<int>> bodyParts = new Dictionary<string, List<int>>();
    private float hoverStartTime;

    private void Start()
    {   
        // Assign the body parts for each food type
        foodType = gameObject.name;
        AssignBodyParts();
        // Set the hover start time and initial position
        hoverStartTime = Time.time;
        initialXPosition = transform.position.x;
        initialZPosition = transform.position.z;

    }
    // Update is called once per frame
    void Update()
    {
        // Hover and rotate
        HoverAndRotate();
    }

    // Hover and rotate
    void HoverAndRotate()
    {
        // Keep the object upright
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // raycast down to determine the ground height
        RaycastHit hit;
        // if the raycast hits the ground then hover
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {   
            //distance to ground
            float distanceToGround = hit.distance;

            //hover offset
            float hoverOffset = hoverAmplitude * Mathf.Sin(hoverFrequency * (Time.time - hoverStartTime));
            //set the position of the object
            transform.position = new Vector3(initialXPosition, hit.point.y + hoverHeight + hoverOffset, initialZPosition);
        }

        // Rotate slowly
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    // Assign the body parts for each food type
    void AssignBodyParts()
    {
        if (foodType.Contains("beetroot"))
        {
            bodyParts.Add("Leg", new List<int> { 0, 1 });
            bodyParts.Add("Head", new List<int> { 0 });
            bodyParts.Add("Arm", new List<int> { 0, 1 });
            bodyParts.Add("Chest", new List<int> { 0 });
        }
        else if (foodType.Contains("cacti"))
        {
            bodyParts.Add("Leg", new List<int> { 2, 3 });
            bodyParts.Add("Head", new List<int> { 1 });
            bodyParts.Add("Arm", new List<int> { 2, 3 });
            bodyParts.Add("Chest", new List<int> { 1 });
        }
        else if (foodType.Contains("carrot"))
        {
            bodyParts.Add("Leg", new List<int> { 4, 5 });
            bodyParts.Add("Head", new List<int> { 2 });
            bodyParts.Add("Arm", new List<int> { 4, 5 });
            bodyParts.Add("Chest", new List<int> { 2 });
        }
        else if (foodType.Contains("corn"))
        {
            bodyParts.Add("Leg", new List<int> { 6 });
            bodyParts.Add("Head", new List<int> { 1 });
            bodyParts.Add("Arm", new List<int> { 0, 1, 2, 3, 4, 5 });
            bodyParts.Add("Chest", new List<int> { 3 });
        }
        else if (foodType.Contains("onion"))
        {
            bodyParts.Add("Leg", new List<int> { 1, 3, 5 });
            bodyParts.Add("Head", new List<int> { 0, 1, 2 });
            bodyParts.Add("Arm", new List<int> { 2, 4 });
            bodyParts.Add("Chest", new List<int> { 0, 1, 2, 3 });
        }
        else if (foodType.Contains("potato"))
        {
            bodyParts.Add("Leg", new List<int> { 0, 2, 4, 6 });
            bodyParts.Add("Head", new List<int> { 0, 1 });
            bodyParts.Add("Arm", new List<int> { 1, 3, 5 });
            bodyParts.Add("Chest", new List<int> { 1, 3 });
        }
        else if (foodType.Contains("tomato"))
        {
            bodyParts.Add("Leg", new List<int> { 1, 3, 5, 6 });
            bodyParts.Add("Head", new List<int> { 2 });
            bodyParts.Add("Arm", new List<int> { 0, 2, 4 });
            bodyParts.Add("Chest", new List<int> { 0, 2 });
        }
        else if (foodType.Contains("wheat"))
        {
            bodyParts.Add("Leg", new List<int> { 0, 2, 4, 6 });
            bodyParts.Add("Head", new List<int> { 1, 2 });
            bodyParts.Add("Arm", new List<int> { 1, 3, 5 });
            bodyParts.Add("Chest", new List<int> { 1, 3 });
        }
    }

    // Get the body part prefabs
    public List<GameObject> GetBodyPartPrefabs(string bodyPart)
    {
        // Check if the body part exists
        if (bodyParts.ContainsKey(bodyPart))
        {
            // Get the indices of the body part prefabs
            List<int> indices = bodyParts[bodyPart];
            List<GameObject> prefabs = new List<GameObject>();

            //switch statement to get the prefabs
            switch (bodyPart)
            {
                case "Leg":
                    foreach (int index in indices)
                    {
                        prefabs.Add(LegsPrefab[index]);
                    }
                    break;
                case "Head":
                    foreach (int index in indices)
                    {
                        prefabs.Add(HeadPrefab[index]);
                    }
                    break;
                case "Arm":
                    foreach (int index in indices)
                    {
                        prefabs.Add(ArmsPrefab[index]);
                    }
                    break;
                case "Chest":
                    foreach (int index in indices)
                    {
                        prefabs.Add(ChestPrefab[index]);
                    }
                    break;
            }

            // Return the prefabs
            return prefabs;
        }
        else
        {
            // Return null if the body part does not exist
            return null;
        }
    }

    //get the body parts dictionary
    public Dictionary<string, List<int>> GetBodyParts()
    {   
        //return the body parts dictionary
        return bodyParts;
    }
}
