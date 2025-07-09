using Framework.System;
using Model;
using QFramework;
using UnityEngine;
using MoveModel = Model.MoveModel;

public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override void Init()
    {
        this.RegisterModel(new GameModel());
        this.RegisterModel(new HealthModel());
        this.RegisterModel(new MoveModel());
        this.RegisterModel(new LevelModel());
        this.RegisterModel(new SkillModel());
        this.RegisterModel(new WeaponModel());
        this.RegisterModel(new ProgressModel());
        this.RegisterSystem(new ProgressSystem());
        this.RegisterSystem(new SkillSystem());
        this.RegisterUtility(new Storage());
    }
}