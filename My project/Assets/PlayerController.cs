using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 11f;
    public float jumpForce = 5f;

    public Transform playerCamera;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded = true;

    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    public float cameraSmoothSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }


    void FixedUpdate()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(playerCamera.right, Vector3.up).normalized;

        Vector3 move = forward * moveZ + right * moveX;

        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + transform.TransformDirection(cameraOffset);
        playerCamera.position = Vector3.Lerp(playerCamera.position, desiredPosition, Time.deltaTime * cameraSmoothSpeed);
        playerCamera.LookAt(transform.position + Vector3.up * 1.5f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
                isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
