using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RocketLanding : MonoBehaviour
{
    [Header("Landing Settings")]
    public Transform landingTarget;
    [SerializeField] AudioSource rocketAudio;
    [SerializeField] AudioClip landClip;
    public float maxSpeed = 10f;
    public float minSpeed = 0.5f;
    public float slowDownDistance = 10f;
    public float landingThreshold = 0.1f;

    [Header("Events")]
    public UnityEvent onLandingComplete;

    private bool isLanding = false;
    public static bool hasLanded = false;

    private void Start()
    {
    }

    void Update()
    {
        if (isLanding)
        {
            Vector3 direction = landingTarget.position - transform.position;
            float distance = direction.magnitude;

            if (distance < landingThreshold)
            {
                Debug.Log("Threshold reached");
                rocketAudio.playOnAwake = false;
                rocketAudio.loop = false;
                rocketAudio.Stop();
                rocketAudio.PlayOneShot(landClip);
                transform.position = landingTarget.position;
                isLanding = false;

                StartCoroutine(StopLanding());
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
        rocketAudio.Play();
    }

    public IEnumerator StopLanding()
    {
        yield return new WaitForSeconds(2f);
        hasLanded = true;
        Debug.Log("Landing Stoppped");
        onLandingComplete?.Invoke();
    }
}
