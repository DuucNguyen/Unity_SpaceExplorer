using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlanetSpawner : MonoBehaviour
{
    public GameObject[] planets;

    private List<GameObject> availablePlanets;

    private float timer;
    public float SpawnRate = 10f;

    void Start()
    {
        availablePlanets = new List<GameObject>();
        SetupPlanet();
        SpawnPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        SetupPlanet();
        if (timer < SpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPlanet();
            timer = 0;
        }
    }

    private void SetupPlanet()
    {
        foreach (var planet in planets)
        {
            if (planet != null
                && !availablePlanets.Contains(planet)
                )
            {
                availablePlanets.Add(planet);
            }
        }
    }

    void SpawnPlanet()
    {
        try
        {
            //find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

            var planet = GetRandomPlanet();

            if (planet != null)
            {
                Instantiate(planet, new Vector3(transform.position.x + 5, Random.Range(min.y, max.y), 0), transform.rotation);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message + " -> " + ex.StackTrace);
        }
    }

    GameObject GetRandomPlanet()
    {

        var randomIndex = Random.Range(0, availablePlanets.Count);

        var planet = availablePlanets.ElementAt(randomIndex);

        //set random speed
        planet.GetComponent<PlanetScript>().speed = Random.Range(0.3f, 0.6f);

        //set random scale
        var scale = Random.Range(0.2f, 1.5f);
        planet.transform.localScale = new Vector2(scale, scale);

        return planet;
    }
}
