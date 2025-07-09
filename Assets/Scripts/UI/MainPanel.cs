using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Framework.Event;
using Model;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MainPanel : BasePanel,IController
{ 
    public Image expBarImage;
    public TMP_Text expBarText;
    public TMP_Text goldCoinText;
    public TMP_Text killText;
    public TMP_Text progressText;
    public TMP_Text timerText;
    public Button progressStartBtn;
    public Button startBtn;
    int exp;
    bool isTiming;
    private GameModel gameModel;
    private LevelModel levelModel;
    private ProgressModel progressModel;
    private void Awake()
    {
        expBarImage = transform.Find("ExpBar/Exp").GetComponent<Image>();
        expBarText = transform.Find("ExpBar/Text").GetComponent<TMP_Text>();
        goldCoinText = transform.Find("MoneyNum/Text").GetComponent<TMP_Text>();
        killText = transform.Find("KillNum/Text").GetComponent<TMP_Text>();
        progressText = transform.Find("Progress").GetComponent<TMP_Text>();
        timerText = transform.Find("Timer").GetComponent<TMP_Text>();
        progressStartBtn = transform.Find("ProgressStartBtn").GetComponent<Button>();
    }
    private void OnEnable()
    {
    }
    void Start()
    {
        gameModel = this.GetModel<GameModel>();
        levelModel = this.GetModel<LevelModel>();
        progressModel = this.GetModel<ProgressModel>();
        //初始化
        //Game
        gameModel.GoldCoinNum.RegisterWithInitValue(newValue =>
        {
            GoldCoinChange(newValue);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        gameModel.KillNum.RegisterWithInitValue(newValue =>
        {
            KillNumChange(newValue);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        //Level
        levelModel.CurrentExp.RegisterWithInitValue(currentExp =>
        {
            Debug.Log("当前等级:" + levelModel.CurrentLevel.Value);
            Debug.Log("当前经验值:"+ currentExp);
            Debug.Log("下一级经验值:" + levelModel.ExpToNextLevel.Value);
            ExpBarChange(levelModel.CurrentLevel.Value,currentExp,levelModel.ExpToNextLevel.Value);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        //Progress
        progressModel.CurrentProgress.RegisterWithInitValue(ProgressChange);
        this.RegisterEvent<ProgressOverEvent>(e =>
        {
            ProgressOver();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        progressStartBtn.gameObject.SetActive(false);
        progressStartBtn.onClick.AddListener(OnProgressStartBtn);
        startBtn.onClick.AddListener(OnStartBtn);
    }

    
    private void OnDestroy()
    {
        levelModel = null;
        gameModel = null;
        progressModel = null;
    }
    private void OnStartBtn()
    {
        isTiming = true;
        SoundManager.Instance.PlaySound(SoundConst.Click);
        GameManager.Instance.GameStart();
        startBtn.gameObject.SetActive(false);
    }

    private void ProgressOver()
    {
        isTiming = false;
        ShowProgressStartBtn();
    }


    //显示按钮
    private void ShowProgressStartBtn()
    {
        if (progressStartBtn != null)
        {
            progressStartBtn.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Progress Start Button has been destroyed!");
        }
    }

    //下一波按钮
    private void OnProgressStartBtn()
    {
        isTiming = true;
        SoundManager.Instance.PlaySound(SoundConst.Click);
        this.SendCommand<IncreaseProgressCommand>();
        //EventCenter.Instance.EventTrigger(EventType.ProgressStart);
        progressStartBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        TimerChange();
    }

    private void TimerChange()
    {
        if (isTiming)
        {
            GameManager.Instance.timer += Time.deltaTime;
            float seconds = Mathf.FloorToInt(GameManager.Instance.timer % 60);
            float minutes = Mathf.FloorToInt((GameManager.Instance.timer / 60) % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
    private void ProgressChange(int num)
    {
        progressText.text = $"Progress:{num}";
    }

    
    public void ExpBarChange(int currentLevel,int currentExp,int expToNextLevel)
    {
        expBarText.text = $"等级{currentLevel}";
        expBarImage.fillAmount = (float)currentExp / expToNextLevel;
    }
    private void GoldCoinChange(int num)
    {
        goldCoinText.text = num.ToString();
    }

    public void KillNumChange(int num)
    {
        killText.text = num.ToString();
    }
    
}
