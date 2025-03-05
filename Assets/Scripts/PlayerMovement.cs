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

    public GameObject projectile;
    public Transform projectileSpawnPoint;
    

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

    private void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void ThrowBall()
    {
        Debug.Log("THROW BALL");
        GameObject flail = Instantiate(projectile, transform);
    }

    private void FixedUpdate()
    {
        moveSpeed = isRunning ? runSpeed : walkSpeed;
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = transform.forward * movement.z + transform.right * movement.x;
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        animator.SetFloat("Speed", rb.velocity.magnitude);
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

