using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner Instance { get; private set; }

    public GameObject asteroidPrefab;
    public int minAsteroidCount = 5;
    public int maxAsteroidCount = 10;
    public float spawnRadius = 10f;
    public float spawnInterval = 1f; // Czas interwa³u sprawdzania i dostosowywania liczby asteroid

    private List<GameObject> asteroids = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnInitialAsteroids();
        StartCoroutine(SpawnAsteroidsIfNeeded()); // Rozpoczêcie procesu sprawdzania i dodawania/usuwania asteroid
    }

    private void SpawnInitialAsteroids()
    {
        int initialAsteroidCount = Random.Range(minAsteroidCount, maxAsteroidCount + 1);

        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    private IEnumerator SpawnAsteroidsIfNeeded()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int currentAsteroidCount = asteroids.Count;
            int spawnOffset = Random.Range(-3, 4); // Losowy offset dla dodawania/odejmowania asteroid

            if (currentAsteroidCount < minAsteroidCount || currentAsteroidCount + spawnOffset < minAsteroidCount)
            {
                int asteroidsToSpawn = Random.Range(minAsteroidCount - currentAsteroidCount, maxAsteroidCount - currentAsteroidCount + 1);

                for (int i = 0; i < asteroidsToSpawn; i++)
                {
                    SpawnAsteroid();
                }
            }
            else if (currentAsteroidCount > maxAsteroidCount || currentAsteroidCount + spawnOffset > maxAsteroidCount)
            {
                int asteroidsToRemove = Mathf.Clamp(currentAsteroidCount + spawnOffset, minAsteroidCount, maxAsteroidCount) - currentAsteroidCount;

                for (int i = 0; i < asteroidsToRemove; i++)
                {
                    GameObject asteroid = asteroids[0];
                    asteroids.RemoveAt(0);
                    Destroy(asteroid);
                }
            }
        }
    }

    public void SpawnAsteroid()
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
}
