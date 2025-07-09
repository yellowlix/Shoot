using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProgressList",menuName = "Progress/ProgressList")]
public class ProgressList : ScriptableObject
{
    public List<Progress> AllProgressList = new List<Progress>();
}

[System.Serializable]
public class Progress
{
    public int id;
    public List<Wave> WaveList = new List<Wave>();
}

[System.Serializable]
public struct Wave
{
    public float createTime;
    public int num;
    public createType type;
}

public enum createType
{
    one,
    multiple
}