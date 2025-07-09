using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework.System;
using Model;
using QFramework;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoosePanel : GamingPanel
{
    public bool isChoiced;
    public GameObject skillCardPrefab;
    List<int> upgradableSkillIds;
    List<int> learnableSkillIds;
    List<int> include = new List<int>();
    private SkillModel skillModel;
    public override void SetActive(bool active)
    {
        base.SetActive(active);
        if (active)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }
    private void Awake()
    {
        

    }
    private void Start()
    {
        SetChoices();
    }
    public void Update()
    {
    }
    public void SetChoices()
    {
        skillModel = this.GetModel<SkillModel>();
        var skillSystem = this.GetSystem<SkillSystem>();
        foreach (int skillId in skillSystem.lastChoices.OrderBy(x => Random.value).Take(3))
        {
            var skill = skillModel.GetSkillById(skillId);
            GameObject skillCardObject = Instantiate(skillCardPrefab, transform);
            skillCardObject.GetComponent<SkillCard>().Init(skill,this);
        }
    }
}
