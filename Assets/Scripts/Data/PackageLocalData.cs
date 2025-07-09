using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData 
{
    /*private static PackageLocalData instance;*/
    /*public static PackageLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
            return instance;
        }
    }*/
    /*public List<PackageLocalItem> localItems;*/
/*    public void SavePackage()
    {
        Debug.Log("保存武器数据");
        string inventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
        PlayerPrefs.Save();
    }*/
    /*public List<PackageLocalItem> LoadPackage()
    {
        if(localItems != null)
        {
            return localItems;
        }
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            localItems = packageLocalData.localItems;
            return localItems;
        }
        else
        {
            localItems = new List<PackageLocalItem>();
            return localItems;
        }
    }*/
}
[System.Serializable]
public class PackageLocalItem
{
    public string uid;
    public int id;
    public int isEquip;//0为未装备，1为已装备
    public int level;


    public override string ToString()
    {
        return string.Format("[id]:{0}[num]:{1}", id, isEquip);
    }
}