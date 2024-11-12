using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float flySpeed = 1.5f;
    [SerializeField] private int value;

    private GameObject flame;
    private GameObject player;
    private Animator animator;
    private float currentHealth;
    private float deadZone = -10;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flame = GameObject.Find("flame-small");
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            transform.position += (Vector3.left * flySpeed) * Time.deltaTime;
            if (transform.position.x < deadZone || currentHealth <= 0)
            {
                animator.SetTrigger("die");
            }
        }
    }
    public void TakeDamage(float _damage)
    {
        currentHealth -= Mathf.Clamp(_damage, 0, currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var level = SceneManager.GetActiveScene().name;

        switch (level)
        {
            case "Level1":
                {
                    break;
                }
            case "Level2":
                {
                    if (collision.tag.Equals("Player")
                        || collision.tag.Equals("Projectile"))
                    {
                        animator.SetTrigger("die");
                    }
                    break;
                }
            case "Level3":
                {
                    if (collision.tag.Equals("Player"))
                    {
                        animator.SetTrigger("die");
                    }

                    player = GameObject.Find("Player");
                    if (player != null)
                    {
                        if (currentHealth == 0)
                        {
                            player.GetComponent<PlayerScore>().IncreaseScore(value);
                        }
                    }

                    break;
                }
        }
    }

    public void Destroy()
    {
        if (flame != null)
        {
            Destroy(flame);
        }
        Destroy(gameObject);
    }
}
