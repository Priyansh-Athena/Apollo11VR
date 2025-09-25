using UnityEngine;
using UnityEngine.InputSystem; // New Input System

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float speed = 10f;
    public float turnSpeed = 50f;

    [Header("Input Actions (from your Input Action Asset)")]
    public InputActionProperty leftStick;
    public InputActionProperty rightStick;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down; // Helps stability
    }

    void FixedUpdate()
    {
        // Read joystick input from either left or right
        Vector2 leftInput = leftStick.action.ReadValue<Vector2>();
        Vector2 rightInput = rightStick.action.ReadValue<Vector2>();

        // Pick whichever stick is being used (non-zero input)
        Vector2 input = leftInput.magnitude > 0.1f ? leftInput : rightInput;

        // Drive forward/backward
        Vector3 move = transform.right * input.y * speed;
        rb.AddForce(move, ForceMode.Acceleration);

        // Turn left/right
        float turn = input.x * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRot = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRot);
    }
}
