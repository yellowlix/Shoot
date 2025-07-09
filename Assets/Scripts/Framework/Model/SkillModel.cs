using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace Model
{
    public class SkillModel : AbstractModel
    {
        public Dictionary<int, int> LearnedSkillsDic { get; } = new();
        public SkillList Skills;

        protected override void OnInit()
        {
            Skills = Resources.Load<SkillList>("Data/Skill/AllSkillList");
        }

        public void Load()
        {
        }

        public void Save()
        {
        }

        public bool IsLearnedSkill(int skillId) => LearnedSkillsDic.ContainsKey(skillId);
        public int GetSkillLevel(int skillId) => LearnedSkillsDic.TryGetValue(skillId, out var level) ? level : 0;

        public List<LearnedSkillItem> GetSortedLearnedSkills()
        {
            return LearnedSkillsDic
                .Select(pair => new LearnedSkillItem
                {
                    id = pair.Key,
                    level = pair.Value
                })
                .OrderByDescending(item => item.level)
                .ThenByDescending(item => item.id)
                .ToList();
        }

        public void UpgradeSkill(int id)
        {
            if (LearnedSkillsDic.ContainsKey(id))
            {
                LearnedSkillsDic[id]++;
            }
            else
            {
                LearnedSkillsDic.Add(id, 1);
            }
        }

        public Skill GetSkillById(int id) => Skills.AllSkillList.Find(s => s.id == id);

        public List<int> GetUpgradeableSkillIds()
        {
            List<int> result = new();
            foreach (var pair in LearnedSkillsDic)
            {
                var skill = GetSkillById(pair.Key);
                if (skill != null && pair.Value < skill.MaxLevel)
                {
                    result.Add(pair.Key);
                }
            }

            return result;
        }

        public List<int> GetLearnableSkillIds()
        {
            List<int> result = new();
            foreach (var skill in Skills.AllSkillList)
            {
                if (!LearnedSkillsDic.ContainsKey(skill.id))
                {
                    result.Add(skill.id);
                }
            }

            return result;
        }
    }

    public class LearnedSkillItem
    {
        public int id;
        public int level;

        public override string ToString()
        {
            return string.Format("[id]:{0}[level]:{1}", id, level);
        }
    }
}