using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] TMP_Text loadingTxt;
    [SerializeField] CanvasGroup loadingBG;
    public UnityEvent OnShowLoadingComplete, OnHideLoadingComplete;


    public void ShowLoading()
    {
        StartCoroutine(ShowLoadingCouroutine());
    }

    public IEnumerator ShowLoadingCouroutine()
    {
        loadingBG.blocksRaycasts = false;
        loadingBG.DOFade(1f, 0.5f);

        for(int i=0; i<=6; i++)
        {
            loadingTxt.text = "Loading" + new string('.', i % 4);
            yield return new WaitForSeconds(0.25f);
        }

        loadingBG.blocksRaycasts = true;
        OnShowLoadingComplete.Invoke();
    }

    public void HideLoading()
    {
        loadingBG.blocksRaycasts = true;
        loadingBG.DOFade(0f, 2f).OnComplete(() =>
        {
            loadingBG.blocksRaycasts = false; ;
            OnHideLoadingComplete.Invoke();
        });
    }
}
