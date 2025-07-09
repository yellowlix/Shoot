using Command;
using QFramework;
using UnityEngine;

[CreateAssetMenu(fileName = "DashSkill", menuName = "Skills/DashSkill")]
public class DashSkill : Skill
{
    public override void GradeProperty()
    {
        this.SendCommand(new UpgradeDashCoolDownCommand(0.5f));
    }
    
}