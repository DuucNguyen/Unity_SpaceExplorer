using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCombat : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireBallSound;

    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerMovement _movementScript;
    private float cooldownTimer = Mathf.Infinity;

    public float attackDamage = 10;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _movementScript = GetComponent<PlayerMovement>();

        _audioSource.volume = 0.1f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) 
            && cooldownTimer > attackCooldown 
            && _movementScript.CanAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        var fireBall = FindFireBall(); //get available fire ball
        cooldownTimer = 0; //reset timer
        _audioSource.PlayOneShot(fireBallSound);
        fireBalls[fireBall].transform.position = firePoint.position; //set initial position
        fireBalls[fireBall].GetComponent<Projectiles>().SetDirection(Mathf.Sign(transform.localScale.x)); //set new direction
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    

}
