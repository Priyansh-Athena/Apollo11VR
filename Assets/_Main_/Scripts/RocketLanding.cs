using UnityEngine;
using UnityEngine.Events;

public class RocketLanding : MonoBehaviour
{
    [Header("Landing Settings")]
    public Transform landingTarget;
    public float maxSpeed = 10f;
    public float minSpeed = 0.5f;
    public float slowDownDistance = 10f;
    public float landingThreshold = 0.1f;

    [Header("Events")]
    public UnityEvent onLandingComplete;

    private bool isLanding = false;

    private void Start()
    {
        StartLanding();
    }

    void Update()
    {
        if (isLanding)
        {
            Vector3 direction = landingTarget.position - transform.position;
            float distance = direction.magnitude;

            if (distance < landingThreshold)
            {
                transform.position = landingTarget.position;
                isLanding = false;

                // Fire the event
                onLandingComplete?.Invoke();
                return;
            }

            direction.Normalize();
            float t = Mathf.Clamp01(distance / slowDownDistance);
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, t);
            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }

    public void StartLanding()
    {
        isLanding = true;
    }
}
