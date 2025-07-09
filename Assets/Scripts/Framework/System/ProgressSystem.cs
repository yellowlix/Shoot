using Framework.Event;
using Model;
using QFramework;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Framework.System
{
    public class ProgressSystem:AbstractSystem
    {
        private ProgressModel progressModel;
        protected override void OnInit()
        {
            progressModel = this.GetModel<ProgressModel>();
            this.RegisterEvent<EnemyDieEvent>(e =>
            {
                EnemyDie();
            });
        }
        public void EnemyDie()
        {
            progressModel.CurrentProgressEnemys.Value--;
            Debug.Log(progressModel.CurrentProgressEnemys.Value.ToString());
            if (!progressModel.CanGenerate.Value)
            {
                if (progressModel.CurrentProgressEnemys.Value <= 0)
                {
                    if (progressModel.CurrentProgress.Value >= progressModel.MaxProgress.Value)
                    {
                        GameOver();
                    }
                    else
                    {
                        ProgressOver();
                    }
                }
            }
        }

        private void GameOver()
        {
            this.SendEvent<GameOverEvent>();
        }

        private void ProgressOver()
        {
            this.SendEvent<ProgressOverEvent>();
        }

        private void ChangeProgress()
        {
            
        }
    }
}