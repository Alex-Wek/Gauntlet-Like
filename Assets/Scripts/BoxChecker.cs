using UnityEngine;
using System.Collections.Generic;

public class BoxChecker : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(10f, 10f, 10f);
    public float forwardOffset = 3f; // renamed to avoid confusion
    public LayerMask layerMask;
    public List<EnemyProto> enemiesInBox = new List<EnemyProto>();

    private Vector3 BoxCenter => transform.position + transform.forward * forwardOffset;

    void Update()
    {
        Collider[] hits = Physics.OverlapBox(BoxCenter, boxSize / 2f, Quaternion.identity, layerMask);
        enemiesInBox.Clear();
        if(hits.Length > 0){Debug.Log("hits = "+hits.Length);}

        foreach (Collider hit in hits)
        {
            EnemyProto enemy = hit.GetComponent<EnemyProto>();
            if (enemy != null)
            {
                enemiesInBox.Add(enemy);
            }
        }

        if (enemiesInBox.Count > 0)
        {
            Debug.Log("Enemies in box: " + enemiesInBox.Count);
            GetComponent<PlayerCombat>().StartMelee();
        }
        else{
            GetComponent<PlayerCombat>().EndMelee();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(BoxCenter, boxSize);
    }
}
