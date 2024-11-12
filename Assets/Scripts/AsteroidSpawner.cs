using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroids;
    public float SpawnRate = 1.5f;
    private float timer;
    private bool isSpawning = false; //Flag to control spawning

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning) // Only spawn when isSpawning is true
        {
            if (timer < SpawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnAsteroid();
                timer = 0;
            }
        }
    }

    void SpawnAsteroid()
    {
        try
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

            if (asteroids.Length > 0)
            {
                var asteroid = asteroids[Random.Range(0, asteroids.Length)];
                if (asteroid != null)
                {
                    int count = Random.Range(0, 2);
                    while (count < 2)
                    {
                        Instantiate(asteroid, new Vector3(transform.position.x, Random.Range(min.y, max.y), 0), transform.rotation);
                        count++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " -> " + ex.StackTrace);
        }
    }

    public void StartSpawnAsteroid()
    {
        Debug.Log("Spawn Asteroid");
        isSpawning = true;  // Enable spawning
        Invoke("SpawnAsteroid", 5f);
    }

    public void StopSpawnAsteroid()
    {
        Debug.Log("Stop Asteroid");
        isSpawning = false;  // Disable spawning
        CancelInvoke("SpawnAsteroid");
    }
}
