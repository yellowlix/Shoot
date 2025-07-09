using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button exitBtn;
    private void Awake()
    {
        
    }
    private void Start()
    {
        continueBtn.gameObject.SetActive(LocalConfig.CheckUserData());
        startBtn.onClick.AddListener(OnStartBtn);
        continueBtn.onClick.AddListener(OnContinueBtn);
        exitBtn.onClick.AddListener(OnExitBtn);
    }

    private void OnStartBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        SceneManager.LoadSceneAsync("OriginalScene");
        Time.timeScale = 1.0f;
    }

    private void OnContinueBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        SceneController.Instance.LoadGameScene();
    }

    private void OnExitBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        // 如果在运行时
        Application.Quit();

        // 如果在编辑器中
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
