using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Command;
using QFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedSkill", menuName = "Skills/SpeedSkill")]
public class SpeedSkill : Skill
{
    public override void GradeProperty()
    {
        this.SendCommand(new UpgradeMoveSpeedCommand(50));
    }
    
}