using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Body Part Prefabs")]
    public GameObject[] LegsPrefab;
    public GameObject[] HeadPrefab;
    public GameObject[] ArmsPrefab;
    public GameObject[] ChestPrefab;

    private string foodType;
    private Dictionary<string, List<int>> bodyParts = new Dictionary<string, List<int>>();

    private void Start()
    {
        foodType = gameObject.name;
        AssignBodyParts();
    }

void AssignBodyParts()
{
    // Conditions for Food1 - Food8
    if (foodType.Contains("Food1"))
    {
        bodyParts.Add("Legs", new List<int> { 0, 1 });
        bodyParts.Add("Head", new List<int> { 0 });
        bodyParts.Add("Arms", new List<int> { 0, 1 });
        bodyParts.Add("Chest", new List<int> { 0 });
    }
    else if (foodType.Contains("Food2"))
    {
        bodyParts.Add("Legs", new List<int> { 2, 3 });
        bodyParts.Add("Head", new List<int> { 1 });
        bodyParts.Add("Arms", new List<int> { 2, 3 });
        bodyParts.Add("Chest", new List<int> { 1 });
    }
    else if (foodType.Contains("Food3"))
    {
        bodyParts.Add("Legs", new List<int> { 4, 5 });
        bodyParts.Add("Head", new List<int> { 2 });
        bodyParts.Add("Arms", new List<int> { 4, 5 });
        bodyParts.Add("Chest", new List<int> { 2 });
    }
    else if (foodType.Contains("Food4"))
    {
        bodyParts.Add("Legs", new List<int> { 6 });
        bodyParts.Add("Head", new List<int> { 1 });
        bodyParts.Add("Arms", new List<int> { 0, 1, 2, 3, 4, 5 }); 
        bodyParts.Add("Chest", new List<int> { 3 });
    }
    else if (foodType.Contains("Food5"))
    {
        bodyParts.Add("Legs", new List<int> { 1, 3, 5 }); 
        bodyParts.Add("Head", new List<int> { 0, 1, 2 }); 
        bodyParts.Add("Arms", new List<int> { 2, 4 }); 
        bodyParts.Add("Chest", new List<int> { 0, 1, 2, 3 }); 
    }
    else if (foodType.Contains("Food6"))
    {
        bodyParts.Add("Legs", new List<int> { 0, 2, 4, 6 }); 
        bodyParts.Add("Head", new List<int> { 0, 1 }); 
        bodyParts.Add("Arms", new List<int> { 1, 3, 5 }); 
        bodyParts.Add("Chest", new List<int> { 1, 3 }); 
    }
    else if (foodType.Contains("Food7"))
    {
        bodyParts.Add("Legs", new List<int> { 1, 3, 5, 7 }); 
        bodyParts.Add("Head", new List<int> { 2 }); 
        bodyParts.Add("Arms", new List<int> { 0, 2, 4 }); 
        bodyParts.Add("Chest", new List<int> { 0, 2 }); 
    }
    else if (foodType.Contains("Food8"))
    {
        bodyParts.Add("Legs", new List<int> { 0, 2, 4, 6 }); 
        bodyParts.Add("Head", new List<int> { 1, 2 }); 
        bodyParts.Add("Arms", new List<int> { 1, 3, 5 }); 
        bodyParts.Add("Chest", new List<int> { 1, 3 }); 
    }
}

    public List<GameObject> GetBodyPartPrefabs(string bodyPart)
    {
        if (bodyParts.ContainsKey(bodyPart))
        {
            List<int> indices = bodyParts[bodyPart];
            List<GameObject> prefabs = new List<GameObject>();

            switch (bodyPart)
            {
                case "Legs":
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
                case "Arms":
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

            return prefabs;
        }
        else
        {
            return null;
        }
    }

        public Dictionary<string, List<int>> GetBodyParts()
    {
        return bodyParts;
    }
}
