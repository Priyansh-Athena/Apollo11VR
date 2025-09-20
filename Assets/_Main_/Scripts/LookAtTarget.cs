using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // Assign the target in the Inspector
    public Transform target;

    // If you want to offset the rotation (e.g. rotate only Y)
    public bool onlyRotateOnY = false;

    // If true, use the back (-Z) axis instead of forward (+Z)
    public bool useBackAsForward = false;

    void Update()
    {
        if (target == null) return;

        if (onlyRotateOnY)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 dir = targetPosition - transform.position;

            if (useBackAsForward)
                dir = -dir; // flip to use back face

            // Make rotation from direction
            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            Vector3 dir = target.position - transform.position;

            if (useBackAsForward)
                dir = -dir; // flip to use back face

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
