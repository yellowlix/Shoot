using Model;
using QFramework;
using UnityEngine;

namespace Command
{
    public class IncreaseProgressCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            var progressModel = this.GetModel<ProgressModel>();
            progressModel.CurrentWave.Value = 0;
            progressModel.CurrentProgress.Value++;
            Debug.Log(progressModel.CurrentProgress.Value.ToString());
        }
    }

    public class ChangeCanGenerageCommand : AbstractCommand
    {
        private bool value;

        public ChangeCanGenerageCommand(bool value)
        {
            this.value = value;
        }

        protected override void OnExecute()
        {
            var progressModel = this.GetModel<ProgressModel>();
            progressModel.CanGenerate.Value = value;
        }
    }
    public class IncreaseCurrentProgressEnemysCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var progressModel = this.GetModel<ProgressModel>();
            progressModel.CurrentProgressEnemys.Value++;
        }
    }
}