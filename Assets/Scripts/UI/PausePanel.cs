using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : GamingPanel
{
    [SerializeField] private Button reStartBtn;
    [SerializeField] private Button setBtn;
    [SerializeField] private Button exitBtn;
    void Start()
    {
        reStartBtn = transform.Find("Bg/ReStartBtn").GetComponent<Button>();
        setBtn = transform.Find("Bg/SetBtn").GetComponent<Button>();
        exitBtn = transform.Find("Bg/ExitBtn").GetComponent<Button>();
        reStartBtn.onClick.AddListener(OnReStartBtn);
        setBtn.onClick.AddListener(OnSetBtn);
        exitBtn.onClick.AddListener(OnExitBtn);
    }
    
/*    private void OnContinueBtn()
    {
        Time.timeScale = 1.0f;
        *//*Cursor.visible = false;*//*
        ClosePanel();
    }*/

    private void OnReStartBtn()
    {
        //TODO:重写
        SoundManager.Instance.PlaySound(SoundConst.Click);
        // GameManager.Instance.currentProgressEnemys = 0;
        // SceneController.Instance.LoadGameScene();
    }
    private void OnSetBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        UIManager.Instance.OpenPanel(UIConst.SetPanel);
    }

    private void OnExitBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        GameManager.Instance.SaveData();
        SceneController.Instance.ReturnMainScene();
    }

}
