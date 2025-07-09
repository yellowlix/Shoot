using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllSkillList", menuName = "Skills/AllSkillList")]
public class SkillList : ScriptableObject
{
    public List<Skill> AllSkillList = new List<Skill>();

}