using UnityEngine;

public class OrbitChild : MonoBehaviour
{
    [Header("Parent to orbit around")]
    public Transform parent;

    [Header("Orbit Settings")]
    public float orbitSpeed = 30f;          // degrees per second
    public float orbitRadius = 5f;          // distance from parent
    public Vector3 orbitAxis = Vector3.up;  // axis to orbit around (default Y)

    private Vector3 startOffset;

    void Start()
    {
        if (parent == null)
            parent = transform.parent;

        // initial offset at given radius
        startOffset = (transform.position - parent.position).normalized * orbitRadius;
    }

    void Update()
    {
        if (parent == null) return;

        // compute rotation around the axis
        Quaternion rotation = Quaternion.AngleAxis(orbitSpeed * Time.deltaTime, orbitAxis);
        startOffset = rotation * startOffset;

        // set position without changing rotation
        transform.position = parent.position + startOffset;
    }
}
