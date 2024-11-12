using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectiles : MonoBehaviour
{

    [SerializeField] private float speed = 2f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask enemyLayer;

    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;
    private bool hit;
    private float direction;
    private SpriteRenderer spriteRenderer;
    private float lifetime;
    private float attackDamage = 20;


    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (hit)
            return;

        float movementSpeed = speed * Time.deltaTime * direction; //move
        transform.Translate(movementSpeed, 0, 0);

        //count time to set object inactive
        lifetime += Time.deltaTime;
        if (lifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.tag;

        switch (collisionTag)
        {
            case "Obstacle":
                {
                    collision.GetComponent<AsteroidScript>().TakeDamage(attackDamage);
                    projectileHit();
                    break;
                }

            case "Enemy":
                {
                    collision.GetComponent<EnemyScript>().TakeDamage(attackDamage);
                    projectileHit();
                    break;
                }

            case "Player":
                {
                    break;
                }
        }
    }

    private void projectileHit()
    {
        hit = true;
        capsuleCollider2D.enabled = false;
        animator.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0f; // Reset lifetime when firing again

        direction = _direction;
        gameObject.SetActive(true); //when fired set active = true
        hit = false;
        capsuleCollider2D.enabled = true;
        float localScaleX = transform.localScale.x;

        //spriteRenderer.flipX = _direction < 0;

        //flip projectile
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    var position = gameObject.transform.position;
    //    Gizmos.DrawWireSphere(position, 0.8f);
    //}
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
