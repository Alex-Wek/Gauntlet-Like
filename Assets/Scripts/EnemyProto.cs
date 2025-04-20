using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto : MonoBehaviour, EnemyInterface
{
    public float health;
    public float damage;
    private Rigidbody rb;
    private Transform t;
    private Transform player;
    public float moveSpeed;
    public float attackDistance;
    private bool isDead = false;
    private bool isAttacking = false;
    private GameObject spawner;
    public Animator animate; 

    public float destroyTime = 8f;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnGeneration");
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Vector3.Distance(player.position, transform.position) > attackDistance)
        {
            animate.SetBool("isAttack", false);
            SeakPlayer();
        }
        else
        {
            animate.SetBool("isAttack", true);
            Attack();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("THIS MUCH" +damage);
        animate.SetTrigger("Hit");
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //animation die, start despawn timer
        Debug.Log("enemy has died");
        animate.SetBool("isDead", true);
        //wwwspawner.GetComponent<Spawner>().bodyCount -= 1;

        this.enabled = false;
        isDead = true;
        Destroy(gameObject, destroyTime);
    }

    public void SeakPlayer()
    {   if(!isDead){
        isAttacking = false;
        animate.SetFloat("Speed", rb.velocity.magnitude);
        t.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    }

    public void Attack()
    {
            animate.SetTrigger("Attack");
            isAttacking = true;
        
    }

        private void OnTriggerEnter(Collider other)
    {
       
        //Debug.Log("meleeweapon triggered");
        //EnemyInterface enemy = other.GetComponentInParent<EnemyInterface>();

        // if(enemy != null && animator.GetBool("isMelee"))
        // {
        //     Debug.Log("hit with melee ball");
        //     Debug.Log(enemy);
        //     enemy.TakeDamage(damage);
        // }
    }
}
