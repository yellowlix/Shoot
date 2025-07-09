using System.Collections.Generic;
using System.Linq;
using Model;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance
    {
        get
        {
            if (instance == null)
            {

                GameObject root = GameObject.Find("SkillManager");
                instance = root.GetComponent<SkillManager>();
            }
            return instance;
        }
    }
    public List<LearnedSkillItem> learnedSkills;
    public SkillList skillList;
    private List<int> exclude;

    private void Awake()
    {
        instance = this;
        learnedSkills = new List<LearnedSkillItem>();
    }
    private void Start()
    {

    }

    public SkillList GetSkillList()
    {
        if (skillList == null)
        {
            skillList = Resources.Load<SkillList>("Data/Skill/SkillList");
        }
        return skillList;
    }
    public List<LearnedSkillItem> GetLearnedSkills()
    {
        return learnedSkills;
    }

    public void SkillChoice()
    {

        if (!CanGrade(out exclude))
        {
            Debug.Log("Skill is Max");
            return;
        }

        Debug.Log("SkillChoise");
        UIManager.Instance.OpenMultiplePanel(UIConst.SkillChoosePanel);
    }

    public List<LearnedSkillItem> GetSortLearnedSkill()
    {
        List<LearnedSkillItem> learnedSkillItems = learnedSkills;
        learnedSkillItems.Sort(new SkillItemComparer());
        return learnedSkillItems;

    }
    
    public bool CanGrade(out List<int> exclude)
    {
        bool canGrade = false;
        exclude = new List<int>();
        List<int> upgradableSkillIds = new List<int>();
        List<int> learnableSkillIds = new List<int>();
        foreach (var learnedSkill in learnedSkills)
        {
            var skill = GetSkillList().AllSkillList.FirstOrDefault(s => s.id == learnedSkill.id);
            if (skill != null && learnedSkill.level < skill.MaxLevel)
            {
                upgradableSkillIds.Add(skill.id);
                canGrade = true;
            }
        }

        foreach (var skill in GetSkillList().AllSkillList)
        {
            if (!learnedSkills.Any(ls => ls.id == skill.id))
            {
                learnableSkillIds.Add(skill.id);
                canGrade = true;
            }
        }
        exclude.AddRange(upgradableSkillIds);
        exclude.AddRange(learnableSkillIds);
        return canGrade;
    }
}

public class SkillItemComparer : Comparer<LearnedSkillItem>
{
    public override int Compare(LearnedSkillItem a, LearnedSkillItem b)
    {
        int levelComparison = b.level.CompareTo(a.level);
        if (levelComparison == 0)
        {
            return b.id.CompareTo(a.id);
        }
        return levelComparison;
    }
}
