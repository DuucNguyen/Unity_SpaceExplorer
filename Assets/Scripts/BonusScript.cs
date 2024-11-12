using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class BonusScript : MonoBehaviour
{
    [SerializeField] private int value;
    private PlayerScore playerScore;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private float flySpeed = 0.5f;
    private float deadZone = -10;

    void Awake()
    {
        //// This will capture the reference before the player becomes inactive
        //GameObject player = GameObject.FindGameObjectWithTag("Player");

        //if (player != null)
        //{
        //    playerScore = player.GetComponent<PlayerScore>();
        //}
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        //before player active -> inactive in inherarchy
        // Find the PlayerScore component even if the Player GameObject is inactive
        playerScore = FindObjectOfType<PlayerScore>(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            transform.position += (Vector3.left * flySpeed) * Time.deltaTime;
            if (transform.position.x < deadZone)
            {
                DestroyGameObject();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerScore.IncreaseScore(value);
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }


}
