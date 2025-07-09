using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIManager
{
    private static UIManager _instance;
    private Transform _uiRoot;
    // 路径配置字典
    private Dictionary<string, string> pathDict;
    // 预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    // 已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;
    //UI对象池
    private Dictionary<string, Queue<GameObject>> UIObjectPool = new Dictionary<string, Queue<GameObject>>();

    private GameObject UIPool;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = GameObject.Find("--UI--").transform;
                return _uiRoot;
            };
            return _uiRoot;
        }
    }
    public void Reset()
    {
        UIPool = null;
        UIObjectPool.Clear();
        panelDict.Clear();
    }
    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PausePanel, "PausePanel"},
            {UIConst.SkillChoosePanel, "SkillChoosePanel"},
            {UIConst.PackagePanel,"PackagePanel"},
            {UIConst.PropertyPanel,"PropertyPanel"},
            {UIConst.GameOverPanel,"GameOverPanel"},
            {UIConst.MainPanel,"MainPanel" },
            {UIConst.OriginalPanel,"OriginalPanel" },
            /*{UIConst.DialoguePanel,"DialoguePanel" },*/
            {UIConst.SetPanel,"SetPanel" },
            {UIConst.OperationPanel,"OperationPanel" }
        };
    }


    public BasePanel OpenMultiplePanel(string name)
    {
        BasePanel panel = null;
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("界面名称错误，或未配置路径: " + name);
            return null;
        }
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/UI/Panel/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panel.OpenPanel(name);
        return panel;
    }
   
    public bool CheckIsOpen(string name)
    {
        BasePanel panel = null;
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.LogWarning("界面已打开: " + name);
            return true;
        }
        return false;
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // 检查是否已打开
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.LogWarning("界面已打开: " + name);
            return null;
        }

        // 检查路径是否配置
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("界面名称错误，或未配置路径: " + name);
            return null;
        }

        // 使用缓存预制件
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/UI/Panel/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }

        // 打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.LogError("界面未打开: " + name);
            return false;
        }

        panel.ClosePanel();
        return true;
    }

    public GameObject GetPanelObject(string name)
    {
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.LogError("界面名称错误，或未配置路径: " + name);
            return null;
        }
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/UI/Panel/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }
        return GetObject(panelPrefab);
        
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        if (!UIObjectPool.ContainsKey(prefab.name) || UIObjectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            if (UIPool == null)
            {
                UIPool = new GameObject("UIObjectPool");
                UIPool.transform.SetParent(UIRoot);
                UIPool.transform.localPosition = Vector3.zero;
            }
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(UIPool.transform,false);
            }
            _object.transform.SetParent(childPool.transform,false);
        }
        _object = UIObjectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    public void PushObject(GameObject _object)
    {
        string _name = _object.name.Replace("(Clone)", string.Empty);
        if (!UIObjectPool.ContainsKey(_name))
        {
            UIObjectPool[_name] = new Queue<GameObject>();
        }
        _object.SetActive(false);
        UIObjectPool[_name].Enqueue(_object);
    }
    /*public void ShowTip(string tip)
    {
        MenuTipPanel menuTipPanel = OpenPanel(UIConst.MenuTipPanel) as MenuTipPanel;
        menuTipPanel.InitTip(Globals.TipCreateOne);
    }*/
}

public class UIConst
{
    // menu panels
    public const string PausePanel = "PausePanel";
    public const string MainPanel = "MainPanel";
    public const string PackagePanel = "PackagePanel";
    public const string PropertyPanel = "PropertyPanel";
    public const string SkillChoosePanel = "SkillChoosePanel";
    public const string GameOverPanel = "GameOverPanel";
    public const string OriginalPanel = "OriginalPanel";
    /*public const string DialoguePanel = "DialoguePanel";*/
    public const string SetPanel = "SetPanel";
    public const string OperationPanel = "OperationPanel";
}