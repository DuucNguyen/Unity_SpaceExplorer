using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BonusSpawner : MonoBehaviour
{
    public GameObject[] bonuses;
    public float SpawnRate = 10f;
    private float timer;
    private float height = 0.5f;

    private bool isSpawning = false;

    void Start()
    {
    }

    void Update()
    {
        if (isSpawning)
        {
            if (timer < SpawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnBonus();
                timer = 0;
            }
        }
    }

    void SpawnBonus()
    {

        try
        {
            //find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

            // Calculate min and max bounds

            if (bonuses.Length > 0)
            {
                var bonus = bonuses[Random.Range(0, bonuses.Length)];
                if (bonus != null)
                {
                    Instantiate(bonus, new Vector3(transform.position.x, Random.Range(min.y + height, max.y - height), 0), transform.rotation);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " -> " + ex.StackTrace);
        }
    }

    public void StartSpawnBonus()
    {
        Debug.Log("Spawn Bonus");
        isSpawning = true;
        Invoke("SpawnBonus", 5f);
    }

    public void StopSpawnBonus()
    {
        Debug.Log("Stop Bonus");
        isSpawning = false;
        CancelInvoke("SpawnBonus");
    }
}
