using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackageBtn : GamingBtn
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (UIManager.Instance.CheckIsOpen(UIConst.PackagePanel))
        {
            UIManager.Instance.ClosePanel(UIConst.PackagePanel);
        }
        else
        {
            UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        }
    }
}
