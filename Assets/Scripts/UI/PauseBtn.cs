using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseBtn : GamingBtn
{
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
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
        Time.timeScale = 0;
        if (UIManager.Instance.CheckIsOpen(UIConst.PausePanel))
        {
            UIManager.Instance.ClosePanel(UIConst.PausePanel);
            Time.timeScale = 1;
            image.sprite = originalSprite;
        }
        else
        {
            UIManager.Instance.OpenPanel(UIConst.PausePanel);
            image.sprite = pauseSprite;
        }
    }
}
