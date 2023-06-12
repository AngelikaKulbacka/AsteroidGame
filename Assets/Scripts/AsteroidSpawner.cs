using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int asteroidCount = 5;
    public float spawnRadius = 10f;

    private void Start()
    {
        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
        }
    }
}
