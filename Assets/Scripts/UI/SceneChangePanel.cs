using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeCanvas :MonoBehaviour
{
    public static SceneChangeCanvas Instance { get; private set; }
    [SerializeField] private float fadeDuration;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }
        Debug.Log("SceneChangeCanvas:Awake");
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public IEnumerator SceneFadeIn()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
    }
    public IEnumerator SceneFadeOut()
    {
        canvasGroup.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        gameObject.SetActive(false);
    }
}
