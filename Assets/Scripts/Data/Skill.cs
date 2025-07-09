using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject,IController
{
    public int id;
    public string Name;
    public int MaxLevel;
    public Sprite Sprite;
    public string Description;
    public virtual void GradeProperty(){}

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}



