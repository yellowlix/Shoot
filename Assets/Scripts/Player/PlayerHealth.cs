using Command;
using Model;
using QFramework;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IController
{
    private HealthModel healthModel;
    //TODO:这两个或许也可以放到healthModel;
    [SerializeField] private bool canRestore;
    [SerializeField] private float restoreInterval = 1f;
    [SerializeField] private float restoreTimer;
    private void Awake()
    {
        
    }
    void Start()
    {
        healthModel = this.GetModel<HealthModel>();
        //TODO:需要在Game初始中定义
        this.SendCommand(new PlayerHealCommand(healthModel.MaxHp.Value));
        canRestore = true;
        restoreTimer = restoreInterval;
        healthModel.CurrentHp.RegisterWithInitValue(currentHp =>
        {
            Debug.Log("生命值变化：" + currentHp);
            PlayerController.Instance.SetHpBar(currentHp, healthModel.MaxHp.Value);
            if (currentHp <= 0)
            {
                Die();
            }
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
        
        Debug.Log("生命值变化：" + healthModel.CurrentHp.Value);
        Debug.Log("最大生命值：" + healthModel.MaxHp.Value);
        Debug.Log("生命值恢复：" + healthModel.HpRegen.Value);
    }

    void Update()
    {
        Restore();
    }


    public void Hurted(int damage)
    {
        Debug.Log("受伤");
        this.SendCommand(new PlayerHurtCommand(damage));
        //TODO:可以放到Command的监听
        PlayerController.Instance.SetRed();
    }
    private void Die()
    {
        Debug.Log("死掉");
       /* //执行死亡方法
        GameManager.Instance.GameOver();*/
    }

    public void UpgradeMaxHp(int num)
    {
        this.SendCommand(new UpgradeMaxHpCommand(num));
    }
    public void UpgradeRestore(int addRestoreHp)
    {
       //TODO:恢复逻辑命令
    }

    void Restore()
    {
        if (!canRestore) return;
        restoreTimer -= Time.deltaTime;
        if (restoreTimer <= 0f)
        {
            if (healthModel.CurrentHp.Value < healthModel.MaxHp.Value)
            {
                this.SendCommand(new PlayerHealCommand(healthModel.HpRegen.Value));
            }
            restoreTimer = restoreInterval;
        }
    }

    void OnDestroy()
    {
        healthModel = null;
    }
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
