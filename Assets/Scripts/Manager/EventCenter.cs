using System.Collections.Generic;
using UnityEngine.Events;
public interface IEventInfo
{
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


public class EventCenter
{
    private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>();
    private static EventCenter instance;

    public static EventCenter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventCenter();
            }
            return instance;
        }
    }


    public void AddEventListener<T>(string name, UnityAction<T> action)
    {

        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo<T>).actions += action;
        else
            _eventDic.Add(name, new EventInfo<T>(action));
    }


    public void AddEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo).actions += action;
        else
            _eventDic.Add(name, new EventInfo(action));
    }
    
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo<T>).actions -= action;
    }
    
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (_eventDic.ContainsKey(name))
            (_eventDic[name] as EventInfo).actions -= action;
    }
    
    public void EventTrigger<T>(string name, T info)
    {
        if (_eventDic.ContainsKey(name))
            if ((_eventDic[name] as EventInfo<T>).actions != null)
                (_eventDic[name] as EventInfo<T>).actions.Invoke(info);
    }
    
    public void EventTrigger(string name)
    {
        if (_eventDic.ContainsKey(name))
            if ((_eventDic[name] as EventInfo).actions != null)
                (_eventDic[name] as EventInfo).actions.Invoke();
    }
    
    public void Clear()
    {
        _eventDic.Clear();
    }
}

public class EventType
{
    public const string GameStart = "GameStart";
    public const string GameOver = "GameOver";
    public const string ProgressOver = "ProgressOver";
    public const string ProgressStart = "ProgressStart";
    public const string GoldCoinChange = "GoldCoinChange";
    public const string KillNumChange = "KillNumChange";
    public const string AddExp = "AddExp";
    public const string EnemyDie = "EnemyDie";
    public const string ProgressChange = "ProgressChange";
    public const string SkillUpgrade = "SkillUpgrade";
}