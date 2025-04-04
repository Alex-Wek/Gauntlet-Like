using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("meleeweapon triggered");
        Debug.Log(other);
        EnemyInterface enemy = other.GetComponent<EnemyInterface>();
        if(enemy != null)
        {
            Debug.Log("hit with melee ball");
            enemy.TakeDamage(damage);
        }
    }
}
