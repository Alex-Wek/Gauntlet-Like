using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;   

    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float meleeRange = 2f;
    public float meleeCooldown = 0.3f;
    private float lastMeleeTime;
    public LayerMask enemyLayer; //need to set to enemy layer
    public float damage = 50f;
    public float health = 10000;

    private bool isAttacking;
    public float attackTime= 2f;
    private float lastAttackTime = -Mathf.Infinity;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        //looks at last attack time, waits until player can attack again
        if(isAttacking && Time.time >= lastAttackTime + attackTime){
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
        }
    }
    private void OnAttack(InputValue value){
        isAttacking = value.isPressed;
        }
    public void ThrowBall()
    {
       
        GameObject flail = Instantiate(projectile, transform);
    }

    //this on trigger event is coupled with melee attacks
    void OnTriggerEnter(Collider other)
    {
        EnemyInterface enemy = other.GetComponent<EnemyInterface>();
        if(enemy != null)
        {
            animator.SetBool("isMelee", true);
            // enemy.TakeDamage(meleeDamage);
            //animator.SetTrigger("Melee");
        }
    }


    void EndMelee(){
        animator.SetBool("isMelee",false);
    }

    public bool getAttacking(){
        return isAttacking;
    }


}
