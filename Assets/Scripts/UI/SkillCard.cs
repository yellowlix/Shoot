using System.Collections;
using System.Collections.Generic;
using Command;
using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour,IPointerClickHandler,IController
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Skill skill;
    [SerializeField] private SkillChoosePanel skillChoosePanel;
    private void Awake()
    {
        icon = transform.Find("Image").GetComponent<Image>();
        description = transform.Find("Description").GetComponent<TMP_Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Skill skill,SkillChoosePanel skillChoosePanel)
    {
        this.skill = skill;
        this.skillChoosePanel = skillChoosePanel;
        icon.sprite = this.skill.Sprite;
        description.text = this.skill.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        skillChoosePanel.ClosePanel();
        this.SendCommand(new UpgradeSkillCommand(skill));
    }
    
    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
