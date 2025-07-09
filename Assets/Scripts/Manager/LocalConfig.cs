//Application.persistentDataPath配置在这里
using UnityEngine;
//文件读写
using System.IO;
//用于Json序列化和反序列化；
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;

public class LocalConfig
{
    public static void SaveUserData(UserData userData)
    {
        if (!File.Exists(Application.persistentDataPath + "/userData"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/userData");
        }
        //转换用户数据为jsons数据
        string jsonData = JsonConvert.SerializeObject(userData);
        //将json字符串写入文件中（文件名为userData）
        File.WriteAllText(Application.persistentDataPath + "/userData/userData.json", jsonData);
    }
    public static bool CheckUserData()
    {
        string path = Application.persistentDataPath + "/userData/userData.json";
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }
    public static UserData LoadUserData()
    {

        string path = Application.persistentDataPath + "/userData/userData.json";
        if (File.Exists(path))
        {
            //从文本文件中加载Json字符串
            string jsonData = File.ReadAllText(path);
            //将Json字符串转换为用户内存数据
            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);
            return userData;
        }
        else
        {
            return null;
        }
    }

    public static bool ClearUserData()
    {
        string path = Application.persistentDataPath + "/userData/userData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        else
        {
            Debug.Log("....删除失败");
            return false;
        }
    }

}

public class UserData
{
    //等级相关
    public int currentLevel;
    public int expGained;
    public int expToNextLevel;
    //血量相关
    public int maxHp;
    public int currentHp;
    public int restoreHp;
    //武器相关
    public List<PackageLocalItem> localItems = new List<PackageLocalItem>();
    public int maxGunNum;
    //移动相关
    public float speed;
    public float dashTime;
    public float dashSpeed;
    public float dashCoolDown;
    //波次相关
    public int currentProgress;
    public string sceneName;
    public float timer;
    //技能相关
    public List<LearnedSkillItem> learnedSkills = new List<LearnedSkillItem>();
    

    public UserData() { }
}
