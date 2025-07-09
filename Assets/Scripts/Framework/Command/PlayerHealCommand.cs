using Model;
using QFramework;
using UnityEngine;

namespace Command
{
    public class PlayerHealCommand:AbstractCommand
    {
        private int value;
        public PlayerHealCommand(int value)
        {
            this.value = value;
        }
        protected override void OnExecute()
        {
            var healthModel = this.GetModel<HealthModel>();
            healthModel.CurrentHp.Value = Mathf.Min(healthModel.CurrentHp.Value + value, healthModel.MaxHp.Value);
        }
    }
    
    public class PlayerHurtCommand:AbstractCommand
    {
        private int value;

        public PlayerHurtCommand(int value)
        {
            this.value = value;
        }

        protected override void OnExecute()
        {
            Debug.Log("PlayerHurtCommand:" + value);
            var healthModel = this.GetModel<HealthModel>();
            healthModel.CurrentHp.Value = Mathf.Clamp(healthModel.CurrentHp.Value - value,0, healthModel.MaxHp.Value);
        }
    }

    public class UpgradeMaxHpCommand : AbstractCommand
    {
        private int value;

        public UpgradeMaxHpCommand(int value)
        {
            this.value = value;
        }

        protected override void OnExecute()
        {
            var healthModel = this.GetModel<HealthModel>();
            healthModel.MaxHp.Value += value;
            healthModel.CurrentHp.Value += value;
        }
    }

    public class UpgradeHpRegenCommand : AbstractCommand
    {
        private int value;

        public UpgradeHpRegenCommand(int value)
        {
            this.value = value;
        }

        protected override void OnExecute()
        {
            var healthModel = this.GetModel<HealthModel>();
            healthModel.HpRegen.Value += value;
        }
    }
}