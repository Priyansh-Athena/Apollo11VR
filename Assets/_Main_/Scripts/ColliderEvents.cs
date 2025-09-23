using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public UnityEvent<Collider> TriggerEnter, TriggerExit, TriggerStay;
    public UnityEvent<Collision> CollisionEnter, CollisionExit, CollisionStay;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerStay.Invoke(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionExit.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionStay.Invoke(collision);
    }
}
