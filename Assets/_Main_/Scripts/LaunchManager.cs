using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] Loading loading;
    [SerializeField] string spaceSceneName;

    private void Start()
    {
        loading.HideLoading();
    }

    public void OpenSpaceScene()
    {
        SceneManager.LoadScene(spaceSceneName);
    }
}
