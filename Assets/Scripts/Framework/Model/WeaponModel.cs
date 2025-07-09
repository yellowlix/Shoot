using System.Collections.Generic;
using QFramework;

namespace Model
{
    public class WeaponModel:AbstractModel
    {
        public List<EquipGunItem> EquipGuns { get; } = new ();
        public BindableProperty<int> CurrentGunIndex { get; } = new(-1);
        public BindableProperty<int> MaxGunNum { get; } = new(2);
        protected override void OnInit()
        {
        }
        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}