using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float speed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.8f;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float sensitivity = 5f;
    public float maxAngle = 85f;

    [Header("Ground Check Settings")]
    public LayerMask groundMask;

    private CharacterController characterController;
    private Vector3 velocity;
    private float cameraPitch = 0f;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        //Input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        //Rotacion de la camara
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxAngle, maxAngle);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        //Comprobar si el jugador esta en el suelo
        Vector3 origin = characterController.bounds.center - new Vector3(0, characterController.bounds.extents.y + 0.2f, 0);

        float sphereRadius = characterController.radius * 0.9f;

        isGrounded = Physics.CheckSphere(origin, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        //Movimiento del jugador
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        //Normalizar el movimiento para que no sea mas rapido en diagonal
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        //Sprint
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        characterController.Move(move* currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
