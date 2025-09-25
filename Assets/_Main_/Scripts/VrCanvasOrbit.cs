using UnityEngine;

public class VrCanvasOrbit : MonoBehaviour
{
    [Header("Target (usually VR camera / XR Rig head)")]
    public Transform playerHead;

    [Header("Canvas Settings")]
    public float distance = 2.0f;       // How far in front of player
    public float heightOffset = -0.2f;  // Lower canvas slightly below eye level
    public float smoothTime = 0.3f;     // Smooth follow speed

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (playerHead == null) return;

        // Target position: in front of the player's head
        Vector3 targetPos = playerHead.position + playerHead.forward * distance;
        targetPos.y += heightOffset;

        // Smoothly move the canvas
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
