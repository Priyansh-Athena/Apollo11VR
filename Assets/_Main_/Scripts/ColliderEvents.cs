using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public UnityEvent TriggerEnter, TriggerExit, TriggerStay;
    public UnityEvent CollisionEnter, CollisionExit, CollisionStay;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerStay.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionExit.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionStay.Invoke();
    }
}
