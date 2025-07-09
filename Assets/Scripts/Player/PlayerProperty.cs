using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Model;
using QFramework;
using UnityEngine;

public class PlayerProperty : MonoBehaviour,IController
{
    [Header("基础属性")]
    public int luck;
    [Header("无形")]
    public bool isIntangible;
    void Awake()
    {
    }
    private void Start()
    {
        EventCenter.Instance.AddEventListener<int>(EventType.AddExp, AddExp);
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<int>(EventType.AddExp, AddExp);
    }
    public void AddExp(int num)
    {
        this.SendCommand(new IncreasePlayerExpCommand(num));
        TryLevelUp();
    }
    
    public void SetIntangible(bool value)
    {
        isIntangible = value;
    }

    public void TryLevelUp()
    {
        this.SendCommand<LevelUpCommand>();
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
