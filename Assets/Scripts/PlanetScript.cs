using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public float speed = 0.4f;


    private float availableRange = 5;

    Vector2 min;
    Vector2 max;

    // Start is called before the first frame update
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //left-bottom
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            transform.position += (Vector3.left * speed) * Time.deltaTime;
            if (transform.position.x < min.x - availableRange)
            {
                Destroy(gameObject);
            }
        }
    }

}
