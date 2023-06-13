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

    public int maxHealth = 3;
    private int currentHealth;

    public int damagePoints = 1;
    public float doubleClickTime = 0.3f;

    public Material blueMaterial;
    public Material greenMaterial;
    public Material redMaterial;

    private bool canTakeDamage = true;
    private float lastClickTime = 0f;

    private Renderer asteroidRenderer;

    private float timeSinceSpawn;
    private bool shouldResetState;

    private int currentPoints = 0;

    public void Initialize(float radius)
    {
        spawnRadius = radius;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        asteroidRenderer = GetComponent<Renderer>();

        ResetAsteroid();
    }

    private void Start()
    {
        ResetAsteroid();
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= currentLifeTime)
        {
            shouldResetState = true;
        }

        if (shouldResetState)
        {
            ResetAsteroid();
            shouldResetState = false;
        }
    }

    private void ResetAsteroid()
    {
        currentLifeTime = Random.Range(minLifeTime, maxLifeTime);
        transform.position = GetRandomPosition();
        transform.rotation = initialRotation;

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }

        UpdateAsteroidColor();
        UpdateAsteroidSize();

        timeSinceSpawn = 0f;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        return spawnPosition + initialPosition;
    }

    private void UpdateAsteroidColor()
    {
        if (currentHealth == 3)
        {
            asteroidRenderer.material = blueMaterial;
        }
        else if (currentHealth == 2)
        {
            asteroidRenderer.material = greenMaterial;
        }
        else if (currentHealth == 1)
        {
            asteroidRenderer.material = redMaterial;
        }
    }

    private void UpdateAsteroidSize()
    {
        float scale = Mathf.Pow(0.5f, maxHealth - currentHealth);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnMouseDown()
    {
        if (canTakeDamage && Time.time - lastClickTime < doubleClickTime)
        {
            currentHealth -= damagePoints;

            if (currentHealth <= 0)
            {
                // Dodawanie nowych asteroid po zniszczeniu
                int asteroidsToAdd = Random.Range(1, 4); // Losowa iloœæ od 1 do 3
                for (int i = 0; i < asteroidsToAdd; i++)
                {
                    AsteroidSpawner.Instance.SpawnAsteroid();
                }

                PointsDisplay.currentPoints++;
                Destroy(gameObject);
            }
            else
            {
                UpdateAsteroidColor();
                UpdateAsteroidSize();
            }

            lastClickTime = 0f;
            canTakeDamage = false;
        }
        else
        {
            lastClickTime = Time.time;
            canTakeDamage = true;
        }
    }
}
