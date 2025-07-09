using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Model;

public class Cm
{
    [MenuItem("Cmd/重置金币")]
    public static void ResetGold()
    {
        PlayerPrefs.DeleteKey("GoldCoinNum");
    }
    [MenuItem("Cmd/重置生命")]
    public static void ResetHealth()
    {
        PlayerPrefs.DeleteKey("MaxHp");
        PlayerPrefs.DeleteKey("CurrentHp");
        PlayerPrefs.DeleteKey("HpRegen");
        PlayerPrefs.DeleteKey("CurrentExp");
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("ExpToNextLevel");
        PlayerPrefs.DeleteKey("MoveSpeed");
        PlayerPrefs.DeleteKey("DashSpeed");
        PlayerPrefs.DeleteKey("DashCoolDown");
        PlayerPrefs.DeleteKey("DashTime");
    }
    [MenuItem("Cmd/当前技能")]
    public static void ReadSkill()
    {
        // List<LearnedSkillItem> learnedSkills = SkillManager.Instance.GetLearnedSkills();
        // Debug.Log(learnedSkills.Count);
        // foreach (LearnedSkillItem item in learnedSkills)
        // {
        //     Debug.Log($"item.id:{item.id},item.level:{item.level}");
        //     item.ToString();
        // }
    }
    [MenuItem("Cmd/刷新页面")]
    public static void RefreshSkill()
    {
        EventCenter.Instance.EventTrigger(EventType.SkillUpgrade);
    }
    [MenuItem("Cmd/散弹枪就绪")]
    public static void AddGun()
    {
        List<PackageLocalItem> packageLocalItems = PackageManager.Instance.GetPackageLocalItems();
        for(int i = 0;i < 6;i++)
        {
            PackageLocalItem packageLocalItem = new PackageLocalItem();
            packageLocalItem.uid = Guid.NewGuid().ToString();
            packageLocalItem.id = 3;
            packageLocalItem.level = 1;
            packageLocalItem.isEquip = 0;
            packageLocalItems.Add(packageLocalItem);
        }
        
    }
}
