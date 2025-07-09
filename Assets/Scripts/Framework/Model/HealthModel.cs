using System;
using Framework.Event;
using QFramework;
using UnityEngine;

namespace Model
{
    public class HealthModel:AbstractModel
    {
        public BindableProperty<int> MaxHp { get; } = new BindableProperty<int>(100);
        public BindableProperty<int> CurrentHp{ get; } = new BindableProperty<int>(100);
        public BindableProperty<int> HpRegen{ get; } = new BindableProperty<int>(0);
        protected override void OnInit()
        {
            var storage = this.GetUtility<Storage>();
            MaxHp.SetValueWithoutEvent(storage.LoadInt(nameof(MaxHp),100));
            CurrentHp.SetValueWithoutEvent(storage.LoadInt(nameof(CurrentHp),100));
            HpRegen.SetValueWithoutEvent(storage.LoadInt(nameof(HpRegen),0));
            MaxHp.Register((newValue) => storage.SaveInt(nameof(MaxHp), newValue));
            CurrentHp.Register((newValue) => storage.SaveInt(nameof(CurrentHp), newValue));
            HpRegen.Register((newValue) => storage.SaveInt(nameof(HpRegen), newValue));
        }
        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}