using UnityEngine;

public class MoonLanding : MonoBehaviour
{
    [SerializeField] Loading loading;


    private void Start()
    {
        loading.HideLoading();
    }
}
