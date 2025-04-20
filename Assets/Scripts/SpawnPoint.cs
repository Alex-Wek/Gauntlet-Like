using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemy;            // The enemy prefab to spawn
    public float spawnSpeed = 2f;       // Seconds between spawns
    public GameObject spawner;          // The object where enemies will be spawned at
    public int maxBody = 2;           // Maximum number of enemies to spawn
    private int bodyCount = 0; 
    public float SpawnPointHealth = 4f;         // Counter for spawned enemies

    void Awake()
    {
        StartCoroutine(SpawnAtFixedLocation());
    }

    IEnumerator SpawnAtFixedLocation()
    {
        while (true)
        {
            if (bodyCount < maxBody)
            {
                Vector3 spawnPosition = spawner.transform.position;
                Instantiate(enemy, spawnPosition, Quaternion.identity);
                bodyCount += 1;
            }
            yield return new WaitForSeconds(spawnSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
         
    {
        if(other != null && other.CompareTag("Weapon")){

            TakeDamage(1);
            Debug.Log("actia;lly got here");
        }
    }
    }

    public void TakeDamage(int damage)
    {
       SpawnPointHealth -= damage;
        if (SpawnPointHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
