using System.Collections;
using System.Collections.Generic;
using Model;
using QFramework;
using TMPro;
using UnityEngine;

public class PropertyPanel : BasePanel,IController
{
    private HealthModel healthModel;
    private MoveModel moveModel;
    [SerializeField]private List<TMP_Text> propertys = new List<TMP_Text>();
    public void Awake()
    {
        
    }
    private void Start()
    {
        healthModel = this.GetModel<HealthModel>();
        moveModel = this.GetModel<MoveModel>();
        Refresh();
    }
    private void Update()
    {
        //TODO:之后改成接收监听
        Refresh();
    }
    void Refresh()
    {
        propertys[0].text = string.Format("生命值：{0}/{1}",healthModel.CurrentHp.Value,healthModel.MaxHp.Value);
        propertys[1].text = string.Format("每秒恢复生命：{0}", healthModel.HpRegen.Value);
        propertys[2].text = string.Format("速度：{0}", moveModel.MoveSpeed.Value);
        propertys[3].text = string.Format("冲刺冷却：{0}s", moveModel.DashCoolDown.Value);
    }
    
}
