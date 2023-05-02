using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{   
    //variables
    public GameObject[] FoodPrefabs;
    public int numberOfFoodObjects = 50;
    public float spawnRadius = 20f;
    public float spawnCheckInterval = 30f;

    // Start is called before the first frame update
    void Start()
    {   
        //spawn the food objects
        SpawnFoodObjects();
        //keep the food count at the same number
        StartCoroutine(KeepFoodCount());

    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawn the food objects
    private void SpawnFoodObjects()
    {
        //get the number of food types
        int numberOfTypes = FoodPrefabs.Length;
        //get the number of food objects per type
        int foodPerType = numberOfFoodObjects / numberOfTypes;

        //loop through the food types
        for (int i = 0; i < numberOfTypes; i++)
        {
            //loop through the food objects per type
            for (int j = 0; j < foodPerType; j++)
            {
                //get a random position
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    3,
                    Random.Range(-spawnRadius, spawnRadius)
                );
                //spawn the food object
                Instantiate(FoodPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    //keep the food count at the same number
    IEnumerator KeepFoodCount()
    {   
        //loop forever
        while (true)
        {
            //wait for spawnCheckInterval seconds
            yield return new WaitForSeconds(spawnCheckInterval);

            //get the current food count
            int currentFoodCount = GameObject.FindGameObjectsWithTag("Food").Length;
            //get the number of food objects to spawn
            int foodToSpawn = numberOfFoodObjects - currentFoodCount;

            //if there are food objects to spawn
            if (foodToSpawn > 0)
            {   
                //get the number of food types
                int numberOfTypes = FoodPrefabs.Length;
                //get the number of food objects per type
                int foodPerType = foodToSpawn / numberOfTypes;

                //loop through the food types
                for (int i = 0; i < numberOfTypes; i++)
                {
                    //loop through the food objects per type
                    for (int j = 0; j < foodPerType; j++)
                    {
                        //get a random position
                        Vector3 spawnPosition = new Vector3(
                            Random.Range(-spawnRadius, spawnRadius),
                            3,
                            Random.Range(-spawnRadius, spawnRadius)
                        );

                        //spawn the food object
                        Instantiate(FoodPrefabs[i], spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}