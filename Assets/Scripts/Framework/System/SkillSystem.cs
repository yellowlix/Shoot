using System.Collections.Generic;
using System.Linq;
using Framework.Event;
using Model;
using QFramework;
using UnityEngine;

namespace Framework.System
{
    public class SkillSystem:AbstractSystem
    {
        private SkillModel skillModel;
        public Queue<int> skillGradeTimesQue = new();
        public List<int> lastChoices = new();
        protected override void OnInit()
        {
            skillModel = this.GetModel<SkillModel>();
            this.RegisterEvent<UpgradeSkillEvent>(e =>
            {
                RequestSkillGrade();
            });
            this.RegisterEvent<AlreadyUpgradeSkillEvent>(e =>
            {
                TryShowNextSkillSelection();
            });
        }
        public List<int> GetSelectableSkillIds()
        {
            var upgradeable = skillModel.GetUpgradeableSkillIds();
            var learnable = skillModel.GetLearnableSkillIds();
            return upgradeable.Concat(learnable).ToList();
        }
        
        public void RequestSkillGrade()
        {
            skillGradeTimesQue.Enqueue(1);
            Debug.Log("skillque:" + skillGradeTimesQue.Count);
            TryShowNextSkillSelection();
        }

        public void TryShowNextSkillSelection()
        {
            if (UIManager.Instance.CheckIsOpen(UIConst.SkillChoosePanel) || skillGradeTimesQue.Count == 0) return;
            skillGradeTimesQue.Dequeue();
            Debug.Log("skillqueAfter:" + skillGradeTimesQue.Count);
            lastChoices = GetSelectableSkillIds();
            if (lastChoices.Count == 0)
            {
                TryShowNextSkillSelection();
                return;
            }
            UIManager.Instance.OpenPanel(UIConst.SkillChoosePanel);
        }
        public void ShowSkillChoosePanel()
        {
            UIManager.Instance.OpenMultiplePanel(UIConst.SkillChoosePanel);
        }
    }
}