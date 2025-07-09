using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    [SerializeField] private Sprite Open;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private bool canGenerate = true;
    public override void Interact()
    {
        base.Interact();
        if (canGenerate)
        {
            GenerateWeapon();
        }
        

    }
    void GenerateWeapon()
    {
        canGenerate = false;
        PackageLocalItem localItem = GetWeaponLocalItem();
        GameObject weapon = Instantiate(weaponPrefab);
        weapon.transform.position = new Vector3(transform.position.x ,transform.position.y -1,transform.position.z);
        weapon.GetComponent<WeaponInteractable>().Init(localItem);
        Invoke("Disappear", 2f);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
    PackageLocalItem GetWeaponLocalItem()
    {
        List<Item> items = PackageManager.Instance.GetItemByType(ItemType.Weapon);
        int index = Random.Range(0, 3);
        Item item = items[index];
        PackageLocalItem packageLocalItem = new()
        {
            uid = System.Guid.NewGuid().ToString(),
            id = item.id,
            isEquip = 0,
            level = Random.Range(1, 4),
        };
        return packageLocalItem;
    }
}
