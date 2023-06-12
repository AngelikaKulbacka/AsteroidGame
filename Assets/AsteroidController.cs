using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float minLifeTime = 1f;
    public float maxLifeTime = 3f;
    private float currentLifeTime;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float spawnRadius;

    public void Initialize(float radius)
    {
        spawnRadius = radius;
        currentLifeTime = Random.Range(minLifeTime, maxLifeTime);
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Start()
    {
        ResetAsteroid();
    }


    private void Update()
    {
        currentLifeTime -= Time.deltaTime;

        if (currentLifeTime <= 0f)
        {
            ResetAsteroid();
        }
    }

    private void ResetAsteroid()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        transform.position = spawnPosition + initialPosition;
        transform.rotation = initialRotation;
        currentLifeTime = Random.Range(minLifeTime, maxLifeTime);
    }
}
