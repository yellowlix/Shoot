using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find("SceneController");
                instance = root.GetComponent<SceneController>();
                if (instance == null)
                {
                    instance = root.AddComponent<SceneController>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            Debug.LogWarning("SceneController== null");
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("SceneController != this");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void NewGameFirstScene()
    {
        StartCoroutine(NewGameFirstSceneCoroutine());
    }
    public IEnumerator NewGameFirstSceneCoroutine()
    {
        yield return SceneManager.LoadSceneAsync("FirstScene");
        GameManager.Instance.SaveData();
        SetEnterPoint();
        ResetObjectPool();
    }
    public void LoadGameScene()
    {
        StartCoroutine(LoadLastGameSceneCoroutine());
    }
    public IEnumerator LoadLastGameSceneCoroutine()
    {
        yield return SceneManager.LoadSceneAsync("FirstScene");
        GameManager.Instance.LoadData();
        SetEnterPoint();
        ResetObjectPool();
    }
    public void ReturnMainScene()
    {
         StartCoroutine(ReturnMainSceneCoroutine());
    }
    public IEnumerator ReturnMainSceneCoroutine()
    {
        
        Destroy(PlayerController.Instance.gameObject);
        Destroy(PackageManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        yield return SceneManager.LoadSceneAsync("MainScene");
        ResetObjectPool();
        Time.timeScale = 1.0f;
    }
    //public IEnumerator INewGameFirstSceneCoroutine()
    //{
    //    //保存数据
    //    GameManager.Instance.SaveGoldCoin();
    //    //淡入当前场景
    //    /*        yield return StartCoroutine(SceneChangeCanvas.Instance.SceneFadeIn());*/
    //    //异步加载场景
    //    yield return SceneManager.LoadSceneAsync("FirstScene");
    //    //加载数据

    //    //获取目标场景过渡位置
    //    SetEnterPoint();
    //    //重置对象池
    //    ResetObjectPool();
    //    //打开主页面

    //    /*GameManager.Instance.RefreshMainPanel();*/
    //    //*/淡入新场景
    //    /* yield return StartCoroutine(SceneChangeCanvas.Instance.SceneFadeOut());*/
    //    //开始游戏*/
    //    GameManager.Instance.GameStart();

    //}


    private void SetEnterPoint()
    {
        //设置玩家位置
        Transform enterPoint = GameObject.Find("EnterPoint").transform;
        PlayerController.Instance.transform.position = enterPoint.position;
    }
    private void ResetObjectPool()
    {
        UIManager.Instance.Reset();
        ObjectPool.Instance.Reset();
        Debug.Log("已经清除");
    }
}

