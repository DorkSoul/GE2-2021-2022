using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public string foodType;
    public List<string> bodyParts = new List<string>();

    void Awake()
    {
        foodType = gameObject.name;
    }
    void Start()
    {
        AssignBodyParts();
    }

    void AssignBodyParts()
    {
        if (foodType.Contains("Food1"))
        {
            bodyParts.AddRange(new string[] { "Ribs", "PartB", "PartC", "PartD", "PartE" });
        }
        else if (foodType.Contains("Food2"))
        {
            bodyParts.AddRange(new string[] { "Ribs 1", "PartG", "PartH", "PartI", "PartJ" });
        }
        else if (foodType.Contains("Food3"))
        {
            bodyParts.AddRange(new string[] { "Ribs 2", "PartL", "PartM", "PartN", "PartO" });
        }
        else if (foodType.Contains("Food4"))
        {
            bodyParts.AddRange(new string[] { "Ribs 2", "PartQ", "PartR", "PartS", "PartT" });
        }
        else if (foodType.Contains("Food5"))
        {
            bodyParts.AddRange(new string[] { "Ribs 3", "PartV", "PartW", "PartX", "PartY" });
        }
        else if (foodType.Contains("Food6"))
        {
            bodyParts.AddRange(new string[] { "Ribs 4", "PartAA", "PartAB", "PartAC", "PartAD" });
        }
        else if (foodType.Contains("Food7"))
        {
            bodyParts.AddRange(new string[] { "Ribs 5", "PartAF", "PartAG", "PartAH", "PartAI" });
        }
        else if (foodType.Contains("Food8"))
        {
            bodyParts.AddRange(new string[] { "Ribs 5", "PartAK", "PartAL", "PartAM", "PartAN" });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
