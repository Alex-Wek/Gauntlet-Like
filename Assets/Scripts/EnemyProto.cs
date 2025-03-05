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
    private Animation animation;
    public float attackDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Vector3.Distance(player.position, transform.position) > attackDistance)
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
        animation.Play("gethit");
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
        animation.Play("Death");
        this.enabled = false;
    }

    public void SeakPlayer()
    {
        animation.Play("Walk");
        t.LookAt(player);
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        animation.Play("Attack2");
    }
}
