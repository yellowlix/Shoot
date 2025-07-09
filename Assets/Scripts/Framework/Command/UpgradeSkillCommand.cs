using Framework.Event;
using Model;
using QFramework;

namespace Command
{
    public class UpgradeSkillCommand:AbstractCommand
    {
        private Skill skill;

        public UpgradeSkillCommand(Skill skill)
        {
            this.skill = skill;
        }

        protected override void OnExecute()
        {
            var skillModel = this.GetModel<SkillModel>();
            skillModel.UpgradeSkill(skill.id);
            skill.GradeProperty();
            //TODO：放在关闭界面好还是在这里好
            this.SendEvent<AlreadyUpgradeSkillEvent>();
        }
    }
}