using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public GameObject[] FoodPrefabs;
    public int numberOfFoodObjects = 50;
    public float spawnRadius = 20f;
    public float spawnCheckInterval = 30f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFoodObjects();
        StartCoroutine(KeepFoodCount());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnFoodObjects()
    {
        int numberOfTypes = FoodPrefabs.Length;
        int foodPerType = numberOfFoodObjects / numberOfTypes;

        for (int i = 0; i < numberOfTypes; i++)
        {
            for (int j = 0; j < foodPerType; j++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    3,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                Instantiate(FoodPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    IEnumerator KeepFoodCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCheckInterval);

            int currentFoodCount = GameObject.FindGameObjectsWithTag("Food").Length;
            int foodToSpawn = numberOfFoodObjects - currentFoodCount;

            if (foodToSpawn > 0)
            {
                int numberOfTypes = FoodPrefabs.Length;
                int foodPerType = foodToSpawn / numberOfTypes;

                for (int i = 0; i < numberOfTypes; i++)
                {
                    for (int j = 0; j < foodPerType; j++)
                    {
                        Vector3 spawnPosition = new Vector3(
                            Random.Range(-spawnRadius, spawnRadius),
                            3,
                            Random.Range(-spawnRadius, spawnRadius)
                        );

                        Instantiate(FoodPrefabs[i], spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}