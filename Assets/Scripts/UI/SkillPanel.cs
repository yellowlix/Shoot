using System.Collections;
using System.Collections.Generic;
using Framework.Event;
using Model;
using QFramework;
using UnityEngine;

public class SkillPanel : MonoBehaviour,IController
{
    [SerializeField] private GameObject skillCellPrefab;
    private SkillModel skillModel;
    private void Awake()
    {
        
    }
    private void Start()
    {
        skillModel = this.GetModel<SkillModel>();
        Refresh();
        this.RegisterEvent<AlreadyUpgradeSkillEvent>(e =>
        {
            Refresh();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
    void Refresh()
    {
        for(int i = 0;i < transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach(LearnedSkillItem item in skillModel.GetSortedLearnedSkills())
        {
            GameObject skillCellObject = Instantiate(skillCellPrefab,transform);
            skillCellObject.GetComponent<SkillCell>().Refresh(item);
        }
    }
    private void OnDestroy()
    {
       skillModel = null;
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
