using UnityEngine;

public class Storage:IStorage
{
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public void SaveString(string key, string value)
    {
        throw new System.NotImplementedException();
    }

    public string LoadString(string key, string defaultValue = "")
    {
        throw new System.NotImplementedException();
    }
}