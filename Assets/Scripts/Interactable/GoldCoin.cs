using System.Collections;
using System.Collections.Generic;
using Command;
using QFramework;
using UnityEngine;

public class GoldCoin : Rewardable,IController
{
    public int goldCoinNum = 10;
    public override void Reward()
    {
        /*GameManager.Instance.AddGoldCoin(goldCoinNum);*/
        this.SendCommand(new ChangeGoldCoinCommand(goldCoinNum));
        GameManager.Instance.ShowValueText($"+{goldCoinNum}", transform.position, Color.yellow);
        SoundManager.Instance.PlaySound(SoundConst.Goins_Grab);
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
