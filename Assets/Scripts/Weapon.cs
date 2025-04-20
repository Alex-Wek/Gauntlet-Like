using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 0f;

    private GameObject player;
    private PlayerCombat pc;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerCombat>();
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    // private void OnTriggerEnter(Collider other)
    // {
       
    //     Debug.Log("meleeweapon triggered");
    //     //EnemyInterface enemy = other.GetComponentInParent<EnemyInterface>();

    //     // if(enemy != null && animator.GetBool("isMelee"))
    //     // {
    //     //     Debug.Log("hit with melee ball");
    //     //     Debug.Log(enemy);
    //     //     enemy.TakeDamage(damage);
    //     // }
    // }


    public float getDamage(){
        return pc.damage;
    }
}
