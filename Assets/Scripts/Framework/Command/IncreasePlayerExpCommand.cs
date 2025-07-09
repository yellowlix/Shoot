using Framework.Event;
using Framework.System;
using Model;
using QFramework;

namespace Command
{
    public class IncreasePlayerExpCommand:AbstractCommand
    {
        private int value;

        public IncreasePlayerExpCommand(int value)
        {
            this.value = value;
        }

        protected override void OnExecute()
        {
            var levelModel = this.GetModel<LevelModel>();
            levelModel.CurrentExp.Value += value;
        }
    }

    public class ChangeExpToNextLevelCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            throw new System.NotImplementedException();
        }
    }

    public class LevelUpCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            //此时currentExp不是全部获得的exp，而是当前等级得到的exp
            var levelModel = this.GetModel<LevelModel>();
            while (levelModel.CurrentExp.Value>= levelModel.ExpToNextLevel.Value)
            {
                levelModel.CurrentLevel.Value++;
                levelModel.CurrentExp.Value -= levelModel.ExpToNextLevel.Value;
                //TODO:使用配置表
                // 可根据等级决定升级所需经验
                int next = 1+ 1 * levelModel.CurrentLevel.Value;
                levelModel.ExpToNextLevel.Value = next;
                this.SendEvent<UpgradeSkillEvent>();
            }
        }
    }
}