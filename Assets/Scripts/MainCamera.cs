using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float distanceThreshold = 10f;
    public float cameraSpeed = 5f;
    private Vector3 offset;
    public Transform player;
    private bool moveCamera;

    private void Start()
    {
        
        offset = transform.position - player.position;
        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        float trueDistance = Vector3.Distance(transform.position, targetPosition);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(Vector3.Distance(transform.position, targetPosition));
            Debug.Log("distanceT = " + distanceThreshold + "  realD = " + trueDistance);

        }

        if (Vector3.Distance(transform.position, targetPosition) > distanceThreshold) { moveCamera = true; }
        
        if (moveCamera)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
            if (trueDistance <= 0) { moveCamera = false; }
        }
    }
}
