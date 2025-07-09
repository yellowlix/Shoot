using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Framework.Event;
using Model;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,IController
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find("GameManager");
                instance = root.GetComponent<GameManager>();
            }
            return instance;
        }
    }
    public InputController inputController;
    public bool isGaming;
    public GameObject valueTextPrefab;
    public GameObject TreasurePrefab;
    public float timer;
    private GameModel gameModel;
    private ProgressModel progressModel; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=  this) 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        inputController = new InputController();
    }
    private void OnEnable()
    {
        inputController.Enable();
        
    }
    void Start()
    {
        Debug.Log("Gamemanager开始");
        gameModel = this.GetModel<GameModel>();
        progressModel = this.GetModel<ProgressModel>();
        this.RegisterEvent<GameOverEvent>(e =>
        {
            GameOver();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<ProgressOverEvent>(e =>
        {
            ProgressOver();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        progressModel.CurrentProgress.Register(e =>
        {
            ProgressStart();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        EventCenter.Instance.AddEventListener(EventType.ProgressStart, ProgressStart);
    }
    public void RefreshMainPanel()
    {
        Debug.Log("刷新主界面");
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
    }
    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        inputController.Disable();
        EventCenter.Instance.RemoveEventListener(EventType.ProgressStart, ProgressStart);
        //关闭所有协程
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {

    }
    //游戏开始方法
    public void GameStart()
    {
        Time.timeScale = 1.0f;
        Debug.Log("开始游戏");
        isGaming = true;
        /*RefreshMainPanel();*/
        EventCenter.Instance.EventTrigger(EventType.GameStart);//游戏开始通知
        ProgressStart();
    }
    //波次开始方法
    public void ProgressStart()
    {
        if (!isGaming)
        {
            return;
        }
        OnProgress();
    }
    //波次结束方法
    public void ProgressOver()
    {
        if (!isGaming)
        {
            return;
        }
        GenerateTreasure();
    }
    
    //TODO可以放到System里
    private void GenerateTreasure()
    {
        GameObject treasure = Instantiate(TreasurePrefab);
        treasure.transform.position = PlayerController.Instance.transform.position + new Vector3(-1,0,0);
    }
    //该波次执行任务
    public void OnProgress()
    {
        Progress progress = progressModel.Progresses.AllProgressList.Find(t => t.id == progressModel.CurrentProgress.Value);
        if (progress != null)
        {
            StartCoroutine(IProgressGenerateEnemy(progress));
            this.SendCommand(new ChangeCanGenerageCommand(true));
        }
    }
    //按波次生成僵尸
    IEnumerator IProgressGenerateEnemy(Progress progress)
    {

        foreach (var wave in progress.WaveList)
        {
            yield return new WaitForSeconds(wave.createTime);
            switch (wave.type)
            {
                case createType.one:
                    yield return StartCoroutine(OneByOneGenerateEnemy(wave.num));
                    break;
                case createType.multiple:
                    yield return StartCoroutine(MultipleGenerateEnemy(wave.num));
                    break;
            }
            //currentWave++;
        }
        this.SendCommand(new ChangeCanGenerageCommand(true));
        StopCoroutine(IProgressGenerateEnemy(progress));
    }

    IEnumerator OneByOneGenerateEnemy(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 generatePoint = EnemyManager.Instance.GeneratePoint();
            //在地图上添加记号
            EnemyManager.Instance.DrawWarnCross(generatePoint);
            yield return new WaitForSeconds(1f);
            GameObject enemyObject = EnemyManager.Instance.GenenrateEnemy(generatePoint);
            
            Debug.Log("生成敌人");
        }
    }
    IEnumerator MultipleGenerateEnemy(int num)
    {
        Vector3[] generatePoints = new Vector3[num];
        for (int i = 0; i < num; i++)
        {
            generatePoints[i] = EnemyManager.Instance.GeneratePoint();
            EnemyManager.Instance.DrawWarnCross(generatePoints[i]);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < num; i++)
        {
            GameObject enemyObject = EnemyManager.Instance.GenenrateEnemy(generatePoints[i]);
            this.SendCommand<IncreaseCurrentProgressEnemysCommand>();
        }

    }
    //敌人死亡监听方法
    public void GameOver()
    {
        if (!isGaming)
        {
            return;
        }
        Time.timeScale = 0f;
        isGaming = false;
        UIManager.Instance.OpenPanel(UIConst.GameOverPanel);
        EventCenter.Instance.EventTrigger(EventType.GameOver);
    }

    public void ShowValueText(string text, Vector3 pos, Color color)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        GameObject valueText = UIManager.Instance.GetObject(valueTextPrefab);
        valueText.transform.position = screenPosition;
        valueText.GetComponent<ValueText>().SetValueText(text, color);
    }

    public void SaveData()
    {
        // UserData userData = new UserData();
        // //血量
        // userData.maxHp = PlayerController.Instance.playerHealth.MaxHp;
        // userData.currentHp = PlayerController.Instance.playerHealth.CurrentHp;
        // //经验
        // userData.currentLevel = PlayerController.Instance.playerProperty.currentLevel;
        // userData.expGained = PlayerController.Instance.playerProperty.expGained;
        // userData.expToNextLevel = PlayerController.Instance.playerProperty.expToNextLevel;
        // //速度
        // userData.speed = PlayerController.Instance.playerMovement.speed;
        // userData.dashCoolDown = PlayerController.Instance.playerMovement.dashCoolDown;
        // //装备
        // userData.maxGunNum = PlayerController.Instance.playerWeapon.maxGunNum;
        // //波次
        // userData.currentProgress = Mathf.Clamp(currentProgress,0,maxProgress);
        // userData.localItems = PackageManager.Instance.localItems;
        // //技能
        // userData.learnedSkills = SkillManager.Instance.learnedSkills;
        // userData.timer = timer;
        // userData.sceneName = SceneManager.GetActiveScene().name;
        // SaveGoldCoin();
        // LocalConfig.SaveUserData(userData);
    }

    public void LoadData()
    {
        // UserData userData = LocalConfig.LoadUserData();
        //
        // if (userData != null)
        // {
        //     Debug.Log("userData != null");
        //     //血量
        //     PlayerController.Instance.playerHealth.MaxHp = userData.maxHp;
        //     PlayerController.Instance.playerHealth.CurrentHp = userData.currentHp;
        //     Debug.Log("userData.currentHp" + userData.currentHp);
        //     //经验
        //     PlayerController.Instance.playerProperty.currentLevel = userData.currentLevel;
        //     PlayerController.Instance.playerProperty.expGained = userData.expGained;
        //     PlayerController.Instance.playerProperty.expToNextLevel = userData.expToNextLevel;
        //     //速度
        //     PlayerController.Instance.playerMovement.speed = userData.speed;
        //     PlayerController.Instance.playerMovement.dashCoolDown = userData.dashCoolDown;
        //     //装备
        //     PlayerController.Instance.playerWeapon.maxGunNum = userData.maxGunNum;
        //     //波次
        //     currentProgress = userData.currentProgress;
        //     //技能
        //     SkillManager.Instance.learnedSkills = userData.learnedSkills;
        //     timer = userData.timer;
        //     PackageManager.Instance.LoadLastGamePackageData(userData.localItems);
        //     /*LoadGoldCoin();*/
        //     
        // }
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}

public class TagConst
{
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Interactable = "Interactable";
    public const string Rewardable = "Rewardable";
    public const string Ground = "Ground";
}