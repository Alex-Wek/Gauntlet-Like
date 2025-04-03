using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto : MonoBehaviour, EnemyInterface
{
    public float health;
    private Rigidbody rb;
    private Transform t;
    private Transform player;
    public float moveSpeed;
    private Animation animate;
    public float attackDistance = 0f;
    private bool isDead = false;
    public GameObject spawner;

    public float destroyTime = 8f;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnGeneration");
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
        animate = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Vector3.Distance(player.position, transform.position) > attackDistance && !isDead)
        {
            SeakPlayer();
        }
        else
        {
            Attack();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("THIS MUCH" +damage);
        GetComponent<Animation>().Play("Idle");
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
        GetComponent<Animation>().Play("DeathG");
        spawner.GetComponent<Spawner>().bodyCount -= 1;

        this.enabled = false;
        isDead = true;
        Destroy(gameObject, destroyTime);
    }

    public void SeakPlayer()
    {
        GetComponent<Animation>().Play("Walk");
        t.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        GetComponent<Animation>().Play("Attack2");
    }
}
