using System.Collections;
using System.Collections.Generic;
using Model;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour,IController
{
    public Image icon;
    public TMP_Text level;
    public LearnedSkillItem learnedSkill;
    public Skill skill;

    void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        level = transform.Find("Level").GetComponent<TMP_Text>();
    }
    
    public void Refresh(LearnedSkillItem learnedSkill)
    {
        var skillModel = this.GetModel<SkillModel>();
        this.learnedSkill = learnedSkill;
        //this.skillPanel = skillPanel;
        this.skill = skillModel.GetSkillById(learnedSkill.id);
        icon.sprite = skill.Sprite;
        level.text = learnedSkill.level.ToString();
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
