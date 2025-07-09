using Model;
using QFramework;
using UnityEngine;

namespace Command
{
    public class UpgradeMoveSpeedCommand:AbstractCommand
    {
        private float value;

        public UpgradeMoveSpeedCommand(float value)
        {
            this.value = value;
        }
        
        protected override void OnExecute()
        {
            var moveModel = this.GetModel<MoveModel>();
            moveModel.MoveSpeed.Value += this.value;
        }
    }
    public class UpgradeDashCoolDownCommand:AbstractCommand
    {
        private float value;

        public UpgradeDashCoolDownCommand(float value)
        {
            this.value = value;
        }
        
        protected override void OnExecute()
        {
            var moveModel = this.GetModel<MoveModel>();
            moveModel.DashCoolDown.Value = Mathf.Clamp(moveModel.DashCoolDown.Value - value, 0.5f, 10f);
        }
    }
}