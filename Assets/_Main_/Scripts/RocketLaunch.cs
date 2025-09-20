using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class RocketLaunch : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float launchHeight = 10000f;
    public float launchDuration = 500f;
    public float shakeDuration = 2f;
    public float shakeStrength = 0.2f;

    [Header("Optional")]
    public ParticleSystem flameParticles;
    public ParticleSystem smoke;
    public TMP_Text distanceTxt;
    public AudioSource countDownAudioSource;
    public AudioSource rocketLaunchAudioSource;

    [Header("Events")]
    public UnityEvent OnRocketReachedSpace;


    private bool launchStarted = false;

    public void LaunchRocket()
    {
        StartCoroutine(LaunchRocketCouroutine());
    }

    public IEnumerator LaunchRocketCouroutine()
    {
        distanceTxt.text = "Initiating Launch Sequence...";
        yield return new WaitForSeconds(2.4f);
        countDownAudioSource.Play();
        yield return new WaitForSeconds(4.4f);

        for (int i = 10; i >= 0; i--)
        {
            distanceTxt.text = $"Rocket Launch Starting in T - {i}...";
            yield return new WaitForSeconds(13/10f);
        }

        rocketLaunchAudioSource.Play();

        smoke.gameObject.SetActive(true);
        flameParticles.gameObject.SetActive(true);

        // Engine shake
        transform.DOShakePosition(shakeDuration, strength: shakeStrength, vibrato: 10, randomness: 90)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // Accelerating launch
        transform.DOMoveY(launchHeight, launchDuration)
            .SetEase(Ease.InExpo).OnComplete(() =>
            {
                OnRocketReachedSpace.Invoke();
                Destroy(gameObject);
            }); // smooth, realistic acceleration

        // Optional VFX
        if (flameParticles != null)
            flameParticles.Play();

        if (smoke != null)
        {
            smoke.Play();
        }

        launchStarted = true;
    }

    private void Update()
    {
        if (launchStarted)
        {
            distanceTxt.text = $"Distance to Space: {(80000 * (transform.position.y / (float)launchHeight))}m";
        }
    }
}
