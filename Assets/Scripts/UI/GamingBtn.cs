using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamingBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        /*Cursor.visible = true;*/
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        /*Cursor.visible = false*/;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        /*Cursor.visible = true;*/
        SoundManager.Instance.PlaySound(SoundConst.Click);
    }
}
