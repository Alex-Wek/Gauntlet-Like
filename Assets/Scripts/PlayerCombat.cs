using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;   

    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float meleeRange = 2f;
    public float meleeCooldown = 0.3f;
    private float lastMeleeTime;
    public LayerMask enemyLayer; //need to set to enemy layer
    public int meleeDamage = 1000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAttack(){
        animator.SetTrigger("Attack");
    }
    public void ThrowBall()
    {
        GameObject flail = Instantiate(projectile, transform);
    }

    void OnTriggerEnter(Collider other)
    {
        
  
        EnemyInterface enemy = other.GetComponent<EnemyInterface>();
        if(enemy != null)
        {
            animator.SetBool("isMelee", true);
            //Debug.Log("enemy hit " + other.name);
            animator.SetTrigger("Melee");
        }
    }


    void EndMelee(){
        animator.SetBool("isMelee",false);
    }

}
