﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    protected override void Fire()
    {
        base.Fire();
        SoundManager.Instance.PlaySound(SoundConst.PistolSound);
    }
}
