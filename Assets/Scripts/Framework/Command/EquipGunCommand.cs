using Model;
using QFramework;
using UnityEngine;

namespace Command
{
    public class EquipGunCommand:AbstractCommand
    {
        private Item item;
        private PackageLocalItem localItem;
        private GameObject gunObject;


        public EquipGunCommand(Item item, PackageLocalItem localItem, GameObject gunObject)
        {
            this.item = item;
            this.localItem = localItem;
            this.gunObject = gunObject;
        }

        protected override void OnExecute()
        {
            var weaponModel = this.GetModel<WeaponModel>();
            weaponModel.EquipGuns.Add(new EquipGunItem(item,localItem, gunObject));    
            if (weaponModel.CurrentGunIndex.Value >= 0)
            {
                weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value].gunObject.SetActive(false);
                //TODO:替换
                weaponModel.CurrentGunIndex.Value = weaponModel.EquipGuns.Count - 1;
                
                weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value].gunObject.SetActive(true);
            }
            else
            {
                weaponModel.CurrentGunIndex.Value = 0;
            }
        }
    }

    public class RemoveAllGunCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            var weaponModel = this.GetModel<WeaponModel>();
            weaponModel.EquipGuns.Clear();
            weaponModel.CurrentGunIndex.Value = -1;
        }
    }
    public class RemoveGunCommand:AbstractCommand
    {
        private EquipGunItem equipGunItem;

        public RemoveGunCommand(EquipGunItem equipGunItem)
        {
            this.equipGunItem = equipGunItem;
        }

        protected override void OnExecute()
        {
            var weaponModel = this.GetModel<WeaponModel>();
            weaponModel.EquipGuns.Remove(equipGunItem);
            if(weaponModel.CurrentGunIndex.Value >= weaponModel.EquipGuns.Count)
            {
                weaponModel.CurrentGunIndex.Value = weaponModel.EquipGuns.Count - 1;
                if (weaponModel.CurrentGunIndex.Value >= 0)
                {
                    weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value].gunObject.SetActive(true);
                }
            }
        }
    }
    
    public class SwitchGunCommand:AbstractCommand
    {
        protected override void OnExecute()
        {
            var weaponModel = this.GetModel<WeaponModel>();
            weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value].gunObject.SetActive(false);
            weaponModel.CurrentGunIndex.Value++;
            if (weaponModel.CurrentGunIndex.Value >=  weaponModel.EquipGuns.Count || weaponModel.CurrentGunIndex.Value >= weaponModel.MaxGunNum.Value)
            {
                weaponModel.CurrentGunIndex.Value = 0;
            }
            weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value].gunObject.SetActive(true);
        }
    }
}