using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //variables
    GameObject CharLegs;
    GameObject CharHead;
    GameObject CharArms;
    GameObject CharChest;

    //prefabs
    public GameObject[] LegsPrefab;
    public GameObject[] HeadPrefab;
    public GameObject[] ArmsPrefab;
    public GameObject[] ChestPrefab;
    public Material[] Materials;
    
    public GameObject SpaceShip;
    public GameObject CharContainer;
    // Start is called before the first frame update
    void Start()
    { 
        // randoms the number of characters to spawn
        var NumSelect= 0;
        NumSelect  = Random.Range(1, 9);
        for(int i=0;i<NumSelect;i++)
        {
            //spawns the creatures
            SpawnCharacter();
        }
        for (int i = 0; i < 9; i++)
        {
            //spawns the spaceships
            SpawnSpaceship();
        }
    }
    // spawns the creatures
    public void SpawnCharacter()
    {
        //creates a new container for the character
        GameObject newCharContainer = (GameObject)Instantiate(CharContainer);
        // selects a random prefab for each body part
        CharLegs = (GameObject)Instantiate(LegsPrefab[Random.Range(0, 7)]);
        CharHead = (GameObject)Instantiate(HeadPrefab[Random.Range(0, 3)]);
        CharArms = (GameObject)Instantiate(ArmsPrefab[Random.Range(0, 6)]);
        CharChest = (GameObject)Instantiate(ChestPrefab[Random.Range(0, 3)]);

        //sets the parent of each body part to the container
        CharLegs.transform.SetParent(newCharContainer.transform);
        CharHead.transform.SetParent(newCharContainer.transform);
        CharArms.transform.SetParent(newCharContainer.transform);
        CharChest.transform.SetParent(newCharContainer.transform);
        
        // selects a random material for each body part
        int materialIndex = Random.Range(0, Materials.Length);
        Material randomMaterial = Materials[materialIndex];

        // sets the material of each body part to the random material
        SetMaterialInChildren(CharLegs, randomMaterial);
        SetMaterialInChildren(CharHead, randomMaterial);
        SetMaterialInChildren(CharArms, randomMaterial);
        SetMaterialInChildren(CharChest, randomMaterial);

        // Set the position of the character container object
        float xPos = Random.Range(-10f, 10f);
        float zPos = Random.Range(-10f, 10f);
        newCharContainer.transform.position = new Vector3(xPos, 0f, zPos);
    }
    // spawns the spaceships
    public void SpawnSpaceship()
    {
        //creates a new container for the spaceship
        GameObject newShipContainer = (GameObject)Instantiate(SpaceShip);

        // Set the position of the character container object
        float xPos = Random.Range(-100f, 100f);
        float zPos = Random.Range(-100f, 100f);
        newShipContainer.transform.position = new Vector3(xPos, 100f, zPos);
    }
    // sets the material of each body part to the random material
    void SetMaterialInChildren(GameObject parent, Material material)
    {
        // Get all the renderers in the parent object
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
        // Set the material of all the renderers to the random material
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
