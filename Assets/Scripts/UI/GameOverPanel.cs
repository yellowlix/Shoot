using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button ExitBtn;
    private void Awake()
    {
        replayBtn = transform.Find("Bg/ReplayBtn").GetComponent<Button>();
        ExitBtn = transform.Find("Bg/ExitBtn").GetComponent<Button>();
    }
    void Start()
    {
        replayBtn.onClick.AddListener(OnReplayBtn);
        ExitBtn.onClick.AddListener(OnExitBtn);
    }

    private void OnReplayBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        SceneController.Instance.LoadGameScene();
    }

    private void OnExitBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        LocalConfig.ClearUserData();
        SceneController.Instance.ReturnMainScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
