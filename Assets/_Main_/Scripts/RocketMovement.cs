using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("References")]
    public Transform cameraTransform;

    private void Update()
    {
        MoveRelativeToCamera();
    }

    void MoveRelativeToCamera()
    {
        // Get input from WASD or arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        // No movement input
        if (horizontal == 0 && vertical == 0)
            return;

        // Get camera forward and right directions, ignoring vertical (y) component
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // Combine movement direction
        Vector3 moveDir = (camForward * vertical + camRight * horizontal).normalized;

        // Move the object
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
