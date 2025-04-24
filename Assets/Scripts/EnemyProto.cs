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

    private EnemyHitFlash flash;


    //adding variables to modify enemy behaviour and make it seem more sporadic 

    // Movement personality
    private float swayOffset;
    private float swaySpeed;
    private float movePauseTimer;
    private float pauseChance;
    private float personalMoveSpeed;



    //adding variables to modify enemy behaviour and make it seem more sporadic 


    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnGeneration");
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").transform;
        animate = GetComponent<Animator>();
        flash = GetComponent<EnemyHitFlash>();
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
        flash.FlashWhite();
        Debug.Log("THIS MUCH" +damage);
        animate.SetTrigger("Hit");
        health -= damage;
        if(health <= 0 && !isDead)
        {
            Die();
        }
    }
private void Die()
{
    Debug.Log("enemy has died");
    animate.SetBool("isDead", true);
    animate.SetTrigger("Die");

    // Disable collision and movement
    rb.velocity = Vector3.zero;
    rb.isKinematic = true;
    GetComponent<Collider>().enabled = false;

    // Make sure they're not interactable
    this.enabled = false;
    isDead = true;

    Destroy(gameObject, destroyTime);
}

    public void SeakPlayer()
    {   if(!isDead){
        isAttacking = false;
        animate.SetFloat("Speed", rb.velocity.magnitude);


        ///t.LookAt(player);
        Vector3 lookDir = (player.position - transform.position);
        lookDir.y = 0f; // Keep upright
        Quaternion targetRotation = Quaternion.LookRotation(lookDir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));



        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    }

    public void Attack()
    {
            animate.SetTrigger("Attack");
            isAttacking = true;
     }
}
