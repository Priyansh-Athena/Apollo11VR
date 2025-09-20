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

    void Start()
    {
        loading.HideLoading();
    }

    // Update is called once per frame
    void Update()
    {
        float remainingDistance = (landingTr.position.z - transform.position.z);
        if (remainingDistance < 0f)
        {
            remainingDistance = 0f;
            StartCoroutine(ReachedLandingPointTasks());
        }
        else
        {
            distanceTxt.text = $"Disntace Remaining: {remainingDistance:F0}m";
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
