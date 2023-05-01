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
    
    public GameObject SpaceShip;
    public GameObject CharContainer;
    // Start is called before the first frame update
    void Start()
    { 
        var NumSelect= 0;
        NumSelect  = Random.Range(1, 9);
        for(int i=0;i<NumSelect;i++)
        {
            SpawnCharacter();
        }
        for (int i = 0; i < 9; i++)
        {
            SpawnSpaceship();
        }
    }
    public void SpawnCharacter()
    {
        GameObject newCharContainer = (GameObject)Instantiate(CharContainer);
        CharLegs = (GameObject)Instantiate(LegsPrefab[Random.Range(0, 7)]);
        CharHead = (GameObject)Instantiate(HeadPrefab[Random.Range(0, 3)]);
        CharArms = (GameObject)Instantiate(ArmsPrefab[Random.Range(0, 6)]);
        CharChest = (GameObject)Instantiate(ChestPrefab[Random.Range(0, 3)]);

        CharLegs.transform.SetParent(newCharContainer.transform);
        CharHead.transform.SetParent(newCharContainer.transform);
        CharArms.transform.SetParent(newCharContainer.transform);
        CharChest.transform.SetParent(newCharContainer.transform);

        // Set the position of the character container object
        float xPos = Random.Range(-10f, 10f);
        float zPos = Random.Range(-10f, 10f);
        newCharContainer.transform.position = new Vector3(xPos, 0f, zPos);
    }
    public void SpawnSpaceship()
    {
        GameObject newShipContainer = (GameObject)Instantiate(SpaceShip);

        // Set the position of the character container object
        float xPos = Random.Range(-100f, 100f);
        float zPos = Random.Range(-100f, 100f);
        newShipContainer.transform.position = new Vector3(xPos, 100f, zPos);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
