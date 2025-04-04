using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private Vector2 lookInput; 
    private Rigidbody rb;
    private Transform transform;
    private float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    private bool isRunning;
    public float rotateSpeed;
    public Animator animator;
    private Camera mainCamera;
    
    //need to move these fields into combat script.
    //so split this script up into movement actions and combat actions
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float meleeRange = 2f;
    public float meleeCooldown = 0.3f;
    private float lastMeleeTime;
    public LayerMask enemyLayer; //need to set to enemy layer
    public int meleeDamage = 50;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    private void OnMove(InputValue value)
    {
        Vector2 playerInput = value.Get<Vector2>();
        movement = new Vector3(playerInput.x, 0f, playerInput.y);
        //animator.SetTrigger("walk");
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void OnRun()
    {
        isRunning = !isRunning;
    }

//this is all combat to move to combat script
    private void OnAttack()
    {
        animator.SetTrigger("Attack");
     
    }

    public void ThrowBall()
    {
        GameObject flail = Instantiate(projectile, transform);
    }

    // private void CheckForMeleeAttack()
    // {
    //     if(rb.velocity.magnitude < 0.1f || Time.time < lastMeleeTime + meleeCooldown)
    //     {
    //         return;
    //     }
    //         // Cast a short ray in the movement direction
    //     Vector3 direction = new Vector3(movement.x, 0f, movement.z).normalized;
    //     Vector3 origin = transform.position + Vector3.up * 0.5f; // Slightly above ground
    //     Ray ray = new Ray(origin, direction);

    //     if (Physics.Raycast(ray, out RaycastHit hit, meleeRange, enemyLayer))
    //     {
    //     // You hit an enemy
    //         EnemyInterface enemy = hit.collider.GetComponent<EnemyInterface>();
    //         if(enemy != null)
    //     {
    //         enemy.TakeDamage(meleeDamage);
    //         Debug.Log("enemy hit ");
    //        // Destroy(gameObject);
    //     }
    //     lastMeleeTime = Time.time;
    //     animator.SetTrigger("Melee");
    //     }
            
    // }

     private void FixedUpdate()
    {
        moveSpeed = isRunning ? runSpeed : walkSpeed;
        MovePlayer();
        RotatePlayer();
        //CheckForMeleeAttack();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.forward * movement.z + transform.right * movement.x;
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        animator.SetFloat("Speed", rb.velocity.magnitude);
        //Debug.Log("speed: "+ rb.velocity.magnitude);
        
    }

    private void RotatePlayer()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {

            Vector3 direction = (hitInfo.point - transform.position).normalized;
            direction.y = 0f;
            if(direction!= Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime));
            }

        }
        
    }
}

