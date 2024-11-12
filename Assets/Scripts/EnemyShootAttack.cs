using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShootAttack : MonoBehaviour
{
    public GameObject enemyProjectile;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level3")
        {
            InvokeRepeating("Attack", 0f, 1.5f);
        }
        else
        {
            Invoke("Attack", 1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            //instantiate enemy projectile
            GameObject projectile = Instantiate(enemyProjectile);

            //set initial's position
            projectile.transform.position = transform.position;

            //compute direction toward player
            Vector2 direction = player.transform.position - projectile.transform.position;


            //set direction
            projectile.GetComponent<EnemyProjectile>().SetDirection(direction);
        }
    }
}
