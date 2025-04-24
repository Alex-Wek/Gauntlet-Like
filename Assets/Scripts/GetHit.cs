using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
private Color originalColor;

    private EnemyProto enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyProto>();
    }

    // Update is called once per frame
    void Update()
    {  
    }

//now it makes sense. the collider on the enemy is runnign into the weapon
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.CompareTag("Weapon")){

            enemy.TakeDamage(other.GetComponent<Weapon>().getDamage());
            //Debug.Log("actia;lly got here");
        }
    }
}
