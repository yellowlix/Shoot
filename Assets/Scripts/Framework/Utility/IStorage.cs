using QFramework;

public interface IStorage :IUtility
{
    void SaveInt(string key, int value);
    int LoadInt(string key,int defaultValue);
    void SaveFloat(string key, float value);
    float LoadFloat(string key, float defaultValue);
    void SaveString(string key, string value);
    string LoadString(string key, string defaultValue);
}