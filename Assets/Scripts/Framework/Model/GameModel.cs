using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Model
{
    public class GameModel : AbstractModel
    {
        public BindableProperty<int> GoldCoinNum { get; } = new BindableProperty<int>(0);
        public BindableProperty<int> KillNum { get; } = new BindableProperty<int>(0);

        protected override void OnInit()
        {
            var storage = this.GetUtility<Storage>();
            GoldCoinNum.SetValueWithoutEvent(storage.LoadInt(nameof(GoldCoinNum)));
            KillNum.SetValueWithoutEvent(storage.LoadInt(nameof(KillNum)));
            GoldCoinNum.Register((newValue) => storage.SaveInt(nameof(GoldCoinNum), newValue));
            KillNum.Register((newValue) => storage.SaveInt(nameof(KillNum), newValue));
        }

        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}