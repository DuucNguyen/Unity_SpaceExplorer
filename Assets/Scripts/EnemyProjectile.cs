using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private Vector2 direction;
    private bool isReady;

    private void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            //get current position
            Vector2 position = transform.position;
            //Quaternion rotation = transform.rotation;

            //compute new position
            position += direction * speed * Time.deltaTime;

            //update position + rotation
            transform.position = position;
            RotateTowardDirection();

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left corner
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right corner

            //destroy when enter deadzone
            if (transform.position.x < min.x || transform.position.y < min.y
                || transform.position.x > max.x || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }
        }
    }

    // Rotate the object based on direction
    void RotateTowardDirection()
    {
        // Calculate the angle between the object's direction and the target direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation on the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle - 180);
    }

    //Set direction
    public void SetDirection(Vector2 _direction)
    {
        //set normalized to get unit vector
        direction = _direction.normalized;
        isReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("PLayer") 
            || collision.tag.Equals("Projectile")
            )
        {
            Destroy(gameObject);
        }
    }
   
    
}
