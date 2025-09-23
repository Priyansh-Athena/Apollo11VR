using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Space : MonoBehaviour
{
    [SerializeField] TMP_Text titleTxt, distanceTxt;
    [SerializeField] Transform landingTr;
    [SerializeField] string moonLandingSceneName;
    [SerializeField] Loading loading;

    float actualDistanceBwMoonAndEarth = 384400f;
    float scaledDistanceBwMoonAndEarth;
    bool landingStarted = false;

    void Start()
    {
        scaledDistanceBwMoonAndEarth = landingTr.position.z - transform.position.z;
        loading.HideLoading();
    }

    // Update is called once per frame
    void Update()
    {
        if (!landingStarted)
        {
            float remainingDistance = ((landingTr.position.z - transform.position.z) / scaledDistanceBwMoonAndEarth) * actualDistanceBwMoonAndEarth;
            distanceTxt.text = $"Disntace Remaining: {remainingDistance:F0} km";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LandingPoint"))
        {
            landingStarted = true;
            StartCoroutine(ReachedLandingPointTasks());
        }
    }

    private IEnumerator ReachedLandingPointTasks()
    {
        titleTxt.text = "We have reached the Moon!";

        for (int i = 5; i >= 0; i--)
        {
            distanceTxt.text = $"Moon Landing Starting in <b>{i}</b>";
            yield return new WaitForSeconds(1f);
        }

        loading.ShowLoading();
    }

    public void OpenMoonLandingScene()
    {
        SceneManager.LoadScene(moonLandingSceneName);
    }
}
