using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float SpawnRate = 20f; //1 object every 20 sec
    private float timer;

    private bool isSpawning = false; //Flag to control spawning

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncreaseSpawnRate", 0, 20f);//increase each 20 sec
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
                SpawnEnemy();
                timer = 0;
            }
        }
    }

    void SpawnEnemy()
    {
        try
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

            var enemy = enemies[Random.Range(0, enemies.Length)];
            if (enemy != null)
            {
                Instantiate(enemy, new Vector3(transform.position.x, Random.Range(min.y, max.y), 0), enemy.transform.rotation);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " -> " + ex.StackTrace);
        }
    }

    public void StartSpawnEnemy()
    {
        Debug.Log("Spawn Enemy");
        isSpawning = true;  // Enable spawning
        Invoke("SpawnEnemy", 10f);
    }

    public void StopSpawnEnemy()
    {
        Debug.Log("Stop Enemy");
        isSpawning = false;  // Disable spawning
        CancelInvoke("SpawnEnemy");
    }

    public void IncreaseSpawnRate()
    {
        if (SpawnRate > 1f)
            SpawnRate--;
        if (SpawnRate == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
