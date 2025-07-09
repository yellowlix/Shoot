using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command;
using QFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "RestoreSkill", menuName = "Skills/ReStoreSkill")]
public class RestoreSkill : Skill
{
    public override void GradeProperty()
    {
        this.SendCommand(new UpgradeHpRegenCommand(2));
    }
}