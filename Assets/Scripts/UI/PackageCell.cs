using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Image levelBg;
    [SerializeField] private PackageLocalItem packageLocalItem;
    [SerializeField] private Item item;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Transform detailPage;
    [SerializeField] private PackagePanel packagePanel;
    [SerializeField] private Transform select;
    [SerializeField] private Transform isEquip;
    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        levelBg = transform.Find("Bg").GetComponent<Image>();
        typeText = transform.Find("DetailPage/Type").GetComponent<TMP_Text>();
        levelText = transform.Find("DetailPage/Level").GetComponent<TMP_Text>();
        detailPage = transform.Find("DetailPage");
        select = transform.Find("Select");
        isEquip = transform.Find("IsEquip");

    }
    void Start()
    {
        detailPage.gameObject.SetActive(false);
        select.gameObject.SetActive(false);
    }
    void SetEquip(int i)
    {
        if (i == 0)
        {
            isEquip.gameObject.SetActive(false);
        }
        else
        {
            isEquip.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh(PackageLocalItem localItem,PackagePanel packagePanel)
    {
        //数据初始化
        this.packageLocalItem = localItem;
        this.item = PackageManager.Instance.GetItemById(localItem.id);
        this.packagePanel = packagePanel;
        //信息设置
        if (item != null && item.icon != null && icon != null)
        {
            icon.sprite = item.icon;
        }
        else
        {
            Debug.LogError("Item or icon is null.");
        }
        SetEquip(packageLocalItem.isEquip);
        switch (packageLocalItem.level)
        {
            case 1:
                levelBg.color = new Color(32f / 255f, 80f / 255f, 0);
                levelText.text = "品质：勇者";
                break;
            case 2:
                levelBg.color = new Color(125f / 255f, 7f / 255f, 143f / 255f);
                levelText.text = "品质：史诗";
                break;
            case 3:
                levelBg.color = new Color(143f / 255f, 12f / 255f, 7f / 255f);
                levelText.text = "品质：传说";
                break;
        }
        //详情页设置
        switch (item.itemtype)
        {
            case ItemType.Weapon:
                typeText.text = "类型：武器";
                break;
            case ItemType.Consumable:
                typeText.text = "类型：消耗品";
                break;
        }

        /*foreach(Property property in item.propertyList)
        {
            string propertyStr = "";
            switch (property.propertyType)
            {
                case PropertyType.Attack:
                    propertyStr = "攻击力：";
                    break;
                case PropertyType.Interval:
                    propertyStr = "攻击间隔：";
                    break;
            }
            propertyStr = propertyStr + property.value.ToString();
            TMP_Text propertyText = Instantiate(levelText).GetComponent<TMP_Text>();
            propertyText.transform.SetParent(transform, false);
            propertyText.text = propertyStr;
        }*/
    }


    void ShowDetailPage(bool active)
    {
        if (active)
        {
            detailPage.gameObject.SetActive(true);
            detailPage.SetParent(packagePanel.transform, true);
        }
        else
        {
            detailPage.gameObject.SetActive(false);
            detailPage.SetParent(transform, true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowDetailPage(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShowDetailPage(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.packagePanel.ChooseUid == this.packageLocalItem.uid) return;
        else
        {
            this.packagePanel.ChooseUid = this.packageLocalItem.uid;
            packagePanel.ChangeChooce(this);
        }
        Select(true);
        
    }
    public void Select(bool active)
    {
        select.gameObject.SetActive(active);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.SetParent(packagePanel.transform, true);
        transform.position = Mouse.current.position.ReadValue();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

    }

}
