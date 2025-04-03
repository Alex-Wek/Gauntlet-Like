using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float enemyCount = 100f;
    public float distance = 10f;

    public int bodyCount = 0;
    public int maxBody = 100;

    public GameObject spawner;
    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(SpawnAroundPlayer());
    }

    IEnumerator SpawnAroundPlayer()
    {

                        //spawn n emenies m untis away from player at random locations
            while(true){
                if(bodyCount < maxBody){
                    Vector2 randomDir = Random.insideUnitCircle.normalized;
                    Vector3 offset = new Vector3(randomDir.x, 0, randomDir.y) * distance;
                    Vector3 pos = player.transform.position + offset;
                    Instantiate(enemy, pos, Quaternion.identity);
                    bodyCount += 1;
                }
                yield return new WaitForSeconds(0.3f);
            }
    }

}
