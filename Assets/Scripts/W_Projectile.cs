
using UnityEngine;

public class W_Projectile : MonoBehaviour
{
    private float speed = 50f;
    public float lifetime = 10f;
    private int damage = 10000;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyInterface enemy = other.GetComponent<EnemyInterface>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("enemy hit " + other.name);
           // Destroy(gameObject);
        }
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
