using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private GameObject gameManager;


    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    private float horizontal;
    private float vertical;
    public bool isAlive = true;


    //find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
    Vector2 min; //this is the bottom-left point (corner) of the screen
    Vector2 max; //this is the top-right point (corner) of the screen


    public void Init()
    {
        //Start button pressed
        gameObject.SetActive(isAlive);
    }

    // Start is called before the first frame update
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }
    private void FixedUpdate()
    {
        Vector2 newVelocity =
            new Vector2(horizontal * speed,
                       vertical * speed);


        // Move the player
        rb.velocity = newVelocity;

        // Clamp the player's position after movement
        Vector2 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min.x, max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, min.y, max.y);

        // Apply the clamped position to the rigidbody
        rb.position = clampedPosition;
    }


    public bool CanAttack()
    {
        return isAlive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle")
            || collision.tag.Equals("EnemyProjectile")
            || collision.tag.Equals("Enemy")
            )
        {
            isAlive = false;
            audioSource.volume = 3;
            audioSource.PlayOneShot(dieSound);
            animator.SetTrigger("die");
        }
    }

    public void Die()
    {
        gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
        gameObject.SetActive(isAlive);
    }

}
