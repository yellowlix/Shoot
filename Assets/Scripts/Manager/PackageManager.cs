using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    private static PackageManager instance;
    public static PackageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find("SceneController");
                instance = root.GetComponent<PackageManager>();
                if (instance == null)
                {
                    instance = root.AddComponent<PackageManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField] private ItemList itemList;
    [SerializeField] public List<PackageLocalItem> localItems;
    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogWarning("PackageManager== null");
            instance = this;
        }
        else if(instance != this)
        {
            Debug.LogWarning("PackageManager!= this");
            Destroy(gameObject);
        }
        localItems = new List<PackageLocalItem>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        /*PackageLocalData.Instance.LoadPackage();*/
    }
    public void ClearPackageData()
    {
        Debug.Log("ClearPackageData");
        PlayerController.Instance.playerWeapon.RemoveAllGun();
        PackageManager.Instance.localItems = new List<PackageLocalItem>();
        /*PackageLocalData.Instance.SavePackage();*/
    }
    public void LoadLastGamePackageData(List<PackageLocalItem> localItems)
    {
        Debug.Log("LoadLastGamePackageData");
/*        PlayerController.Instance.playerWeapon.RemoveAllGun();*/
        PackageManager.Instance.localItems = localItems;
        /*PackageLocalData.Instance.SavePackage();*/
        foreach (PackageLocalItem localItem in PackageManager.Instance.localItems)
        {
            Debug.Log(localItem.ToString());
            if(localItem.isEquip == 1)
            {
                PlayerController.Instance.playerWeapon.EquipGun(localItem);
            }
        }
    }
    public ItemList GetItemList()
    {
        if(itemList == null)
        {
            itemList = Resources.Load<ItemList>("Data/Item/ItemList");
        }
        return itemList;
    }
    public List<PackageLocalItem> GetPackageLocalItems()
    {
        return localItems;
    }
    public Item GetItemById(int id)
    {
        List<Item> itemList = GetItemList().AllItemList;
        foreach(Item item in itemList)
        {
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }
    public PackageLocalItem GetPackageLocalItemByUId(string uid)
    {
        List<PackageLocalItem> packageLocalItems = GetPackageLocalItems();
        foreach (PackageLocalItem item in packageLocalItems)
        {
            if (item.uid == uid)
            {
                return item;
            }
        }
        return null;
    }
    public List<Item> GetItemByType(ItemType type)
    {
        List<Item> Items = new List<Item>();
        foreach (Item item in GetItemList().AllItemList)
        {
            if(item.itemtype == type)
            {
                Items.Add(item);
            }
        }
        return Items;
    }
    public List<PackageLocalItem> GetSortPackageLocalData()
    {
        List<PackageLocalItem> localItems = this.localItems;
        localItems.Sort(new PackageItemComparer());
        return localItems;
    }

    public void DeletePackageItem(string uid)
    {
        Debug.Log("删除" + uid);
        PackageLocalItem packageLocalItem = GetPackageLocalItemByUId(uid);
        if (packageLocalItem == null) return;
        PackageManager.Instance.localItems.Remove(packageLocalItem);
        /*PackageLocalData.Instance.SavePackage();*/
        Debug.Log("删除完成");
    }

    
}
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem a,PackageLocalItem b)
    {
        Item x = PackageManager.Instance.GetItemById(a.id);
        Item y = PackageManager.Instance.GetItemById(b.id);
        int isEquipComparison = b.isEquip.CompareTo(a.isEquip);
        if (isEquipComparison == 0)
        {
            int levelComparison = y.id.CompareTo(a.level);
            if (levelComparison == 0)
            {
                return b.id.CompareTo(a.id);
            }
            return levelComparison;
        }
        return isEquipComparison;
    }

    
}
