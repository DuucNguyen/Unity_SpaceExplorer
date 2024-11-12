using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float flySpeed = 1;

    private float currentHealth;
    private float deadZone = -10;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
       if(gameObject!=null)
        {
            transform.position += (Vector3.left * flySpeed) * Time.deltaTime;
            if (transform.position.x < deadZone || currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= Mathf.Clamp(_damage, 0, currentHealth);
    }

    

}
