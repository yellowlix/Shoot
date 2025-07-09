using System.Collections;
using System.Collections.Generic;
using Command;
using QFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "HpSkill", menuName = "Skills/HpSkill")]
public class HpSkill : Skill
{
    public override void GradeProperty()
    {
        this.SendCommand(new UpgradeMaxHpCommand(50));
    }
}