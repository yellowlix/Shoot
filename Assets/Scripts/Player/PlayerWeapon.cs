using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Model;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour,IController
{
    private WeaponModel weaponModel;
    public PackageLocalItem defaultGun;
    private void Awake()
    {
        
    }
    void Start()
    {
        weaponModel = this.GetModel<WeaponModel>();
        GameManager.Instance.inputController.Player.SwitchGun.performed += SwitchGun;
        if (defaultGun != null)
        {
            EquipGun(defaultGun);
        }
    }

    void Update()
    {

    }
    public void RemoveAllGun()
    {
        Debug.Log("移除枪");
        foreach (var item in weaponModel.EquipGuns)
        {
            Destroy(item.gunObject);
        }
        this.SendCommand<RemoveAllGunCommand>();
    }
    public bool EquipGun(PackageLocalItem localItem)
    {
        if(localItem == null)
        {
            Debug.Log("localItem == null");
            return false;
        }
        if (weaponModel.EquipGuns.Count < weaponModel.MaxGunNum.Value)
        {
            //TODO:数值替换
            Item item = PackageManager.Instance.GetItemById(localItem.id);
            GameObject gunObject = Instantiate(item.objectPrefab);
            Gun gun = gunObject.GetComponent<Gun>();
            gun.transform.parent = transform;
            gun.transform.localPosition = gun.equipPos;
            gun.transform.localScale = gun.equipScale;
            Property property = item.levelPropertyList.Find(t => t.level == localItem.level);
            gun.attack = property.attack;
            gun.interval = property.interval;
            
            this.SendCommand(new EquipGunCommand(item, localItem, gunObject));
            localItem.isEquip = 1;
            if (!PackageManager.Instance.localItems.Contains(localItem))
            {
                PackageManager.Instance.localItems.Add(localItem);
            }
            //TODO：可以替换
            PlayerController.Instance.ShowGunName(item.Name, localItem.level);
            /*PackageLocalData.Instance.SavePackage();*/
            return true;
        }
        else
        {
            if (!PackageManager.Instance.localItems.Contains(localItem))
            {
                PackageManager.Instance.localItems.Add(localItem);
            }
            return false;
        }
    }
    public void RemoveGun(PackageLocalItem localItem)
    {
        EquipGunItem equipGunItem = weaponModel.EquipGuns.Find(t => t.localItem.uid == localItem.uid);
        if (equipGunItem != null)
        {
            this.SendCommand(new RemoveGunCommand(equipGunItem));
            Destroy(equipGunItem.gunObject);
            localItem.isEquip = 0;
            /*PackageLocalData.Instance.SavePackage();*/
        }
    }


    void SwitchGun(InputAction.CallbackContext callback)
    {
        if (weaponModel.EquipGuns.Count > 0)
        {
            this.SendCommand<SwitchGunCommand>();
            var currentEquipGun = weaponModel.EquipGuns[weaponModel.CurrentGunIndex.Value];
            PlayerController.Instance.ShowGunName(currentEquipGun.item.Name, currentEquipGun.localItem.level);
            SoundManager.Instance.PlaySound(SoundConst.Pick_Weapon);
        }
    }

    private void OnDestroy()
    {
        weaponModel = null;
    }

    /* private void OnDisable()
     {
         GameManager.Instance.inputController.Player.SwitchGun.performed -= SwitchGun;
     }*/
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}

public class EquipGunItem
{
    public Item item;
    public PackageLocalItem localItem;
    public GameObject gunObject;
    

    public EquipGunItem(Item item, PackageLocalItem localItem, GameObject gunObject)
    {
        this.item = item;
        this.localItem = localItem;
        this.gunObject = gunObject;
    }
}
