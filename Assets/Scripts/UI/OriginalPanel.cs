using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OriginalPanel : BasePanel
{
    [SerializeField] private TMP_Text goldCoinText;
    [SerializeField] private Button setButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button helpButton;
    private GameModel gameModel;
    private void Awake()
    {
        goldCoinText = transform.Find("MoneyNum/Text").GetComponent<TMP_Text>();
        setButton = transform.Find("SetBtn").GetComponent<Button>();
        exitButton = transform.Find("ExitBtn").GetComponent <Button>();
        helpButton = transform.Find("HelpBtn").GetComponent<Button>();
    }
    private void Start()
    {
        gameModel = this.GetModel<GameModel>();
        gameModel.GoldCoinNum.RegisterWithInitValue(GoldCoinChange).UnRegisterWhenGameObjectDestroyed(gameObject);
        setButton.onClick.AddListener(OnSetBtn);
        exitButton.onClick.AddListener(OnExitBtn);
        helpButton.onClick.AddListener(OnHelpBtn);
    }

    private void OnHelpBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        if (UIManager.Instance.CheckIsOpen(UIConst.OperationPanel))
        {
            UIManager.Instance.ClosePanel(UIConst.OperationPanel);
        }
        else
        {
            UIManager.Instance.OpenPanel(UIConst.OperationPanel);
        }
    }

    private void OnSetBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        UIManager.Instance.OpenPanel(UIConst.SetPanel);
    }

    private void OnExitBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        SceneController.Instance.ReturnMainScene();
    }

    private void Update()
    {
       
    }
    public void GoldCoinChange(int num)
    {
        goldCoinText.text = num.ToString();
    }
}
