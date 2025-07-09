using QFramework;
using UnityEngine;

namespace Model
{
    public class ProgressModel:AbstractModel
    {
        public int currentWave = 0;
        public BindableProperty<int> CurrentProgress{ get; } = new(0);//波次，从0开始计数。
        public BindableProperty<int> MaxProgress{ get; } = new(20);
        public BindableProperty<int> CurrentWave{ get; } = new(0);//小波次，从0开始计数。
        public BindableProperty<bool> CanGenerate{ get; } = new(false);
        public BindableProperty<int> CurrentProgressEnemys { get; } = new(0);
        public ProgressList Progresses;
        protected override void OnInit()
        {
            Progresses = Resources.Load<ProgressList>("Data/Progress/ProgressList");
        }
    }
}