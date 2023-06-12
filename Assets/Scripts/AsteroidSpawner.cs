using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int minAsteroidCount = 5;
    public int maxAsteroidCount = 10;
    public float spawnRadius = 10f;

    private List<GameObject> asteroids = new List<GameObject>();

    private void Start()
    {
        SpawnInitialAsteroids();
    }

    private void SpawnInitialAsteroids()
    {
        int initialAsteroidCount = Random.Range(minAsteroidCount, maxAsteroidCount + 1);

        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        GameObject asteroidObject = Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
        AsteroidController asteroidController = asteroidObject.GetComponent<AsteroidController>();

        asteroidController.Initialize(spawnRadius);
        asteroids.Add(asteroidObject);

        // Set asteroid's layer to "AsteroidLayer"
        asteroidObject.layer = LayerMask.NameToLayer("AsteroidLayer");
    }

    private void Update()
    {
        CheckAsteroidCount();
    }

    private void CheckAsteroidCount()
    {
        int currentAsteroidCount = asteroids.Count;

        if (currentAsteroidCount < minAsteroidCount)
        {
            int asteroidsToSpawn = Random.Range(minAsteroidCount - currentAsteroidCount, maxAsteroidCount - currentAsteroidCount + 1);

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                SpawnAsteroid();
            }
        }
        else if (currentAsteroidCount > maxAsteroidCount)
        {
            int asteroidsToRemove = currentAsteroidCount - Random.Range(minAsteroidCount, maxAsteroidCount + 1);

            for (int i = 0; i < asteroidsToRemove; i++)
            {
                GameObject asteroid = asteroids[0];
                asteroids.RemoveAt(0);
                Destroy(asteroid);
            }
        }
    }
}
