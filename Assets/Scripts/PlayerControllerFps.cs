using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 5f, mouseSensitivity = 0.2f;
    public LayerMask groundLayer;
    public Transform playerCamera;

    Rigidbody rb;
    InputAction moveAction, jumpAction, lookAction;
    float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        jumpAction = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");
        lookAction = new InputAction("Look", InputActionType.Value, "<Mouse>/delta");

        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
    }

    void Update()
    {
        if (jumpAction.WasPressedThisFrame() && Physics.CheckSphere(transform.position + Vector3.down, 0.4f, groundLayer))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Vector2 look = lookAction.ReadValue<Vector2>() * mouseSensitivity;
        
        xRotation -= look.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.x);
    }

    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        
        rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
    }
}

//Eylule Notlar:
// Inspectordan yürünebilecek yerlerde Ground Layerını seçmeyi unutma
//Mouseu geri almak için esc tuşuna bas