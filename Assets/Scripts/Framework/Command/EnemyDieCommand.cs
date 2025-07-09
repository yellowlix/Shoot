using Framework.Event;
using Model;
using QFramework;

namespace Command
{
    public class EnemyDieCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<GameModel>();
            var progressModel = this.GetModel<ProgressModel>();
            gameModel.KillNum.Value++;
            progressModel.CurrentProgressEnemys.Value--;
            this.SendEvent<EnemyDieEvent>();
        }
    }
}