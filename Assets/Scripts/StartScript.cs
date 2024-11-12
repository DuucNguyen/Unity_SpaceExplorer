using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class StartScript : MonoBehaviour
{

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get position of start
        Vector2 position = transform.position;

        //Compute new position
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        //Update position
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //left-bottom
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right


        if(transform.position.x < min.x)
        {
            transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
        }
    }
}
