using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    GameObject CharLegs;
    GameObject CharHead;
    GameObject CharArms;
    GameObject CharChest;

    public GameObject[] LegsPrefab;
    public GameObject[] HeadPrefab;
    public GameObject[] ArmsPrefab;
    public GameObject[] ChestPrefab;

    public GameObject CharContainer;
    // Start is called before the first frame update
    void Start()
    { 
        var NumSelect= 0;
        NumSelect  = Random.Range(0, 9);
        for(int i=0;i<3;i++)
        {
            SpawnCharacter();
        }
    }
    public void SpawnCharacter()
    {
        GameObject newCharContainer = (GameObject)Instantiate(CharContainer);
        CharLegs = (GameObject)Instantiate(LegsPrefab[1]);
        CharHead = (GameObject)Instantiate(HeadPrefab[1]);
        CharArms = (GameObject)Instantiate(ArmsPrefab[1]);
        CharChest = (GameObject)Instantiate(ChestPrefab[1]);

        CharLegs.transform.SetParent(newCharContainer.transform);
        CharHead.transform.SetParent(newCharContainer.transform);
        CharArms.transform.SetParent(newCharContainer.transform);
        CharChest.transform.SetParent(newCharContainer.transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
