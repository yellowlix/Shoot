using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpStone : Rewardable
{
    public int exp;
    public override void Reward()
    {
        EventCenter.Instance.EventTrigger<int>(EventType.AddExp, exp);
        GameManager.Instance.ShowValueText($"+{exp}", transform.position, Color.blue);
        SoundManager.Instance.PlaySound(SoundConst.Gem_Grab);
    }
}
