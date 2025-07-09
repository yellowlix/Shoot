using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyBtn : GamingBtn
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (UIManager.Instance.CheckIsOpen(UIConst.PropertyPanel))
        {
            UIManager.Instance.ClosePanel(UIConst.PropertyPanel);
        }
        else
        {
            UIManager.Instance.OpenPanel(UIConst.PropertyPanel);
        }
    }
}
