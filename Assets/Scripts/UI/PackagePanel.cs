using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel :BasePanel
{
    public GameObject PackageItemPrefab;
    public GameObject PackageItemParentPrefab;
    public Transform Grid;
    [SerializeField] List<Transform> PackageItemParentsTransform = new List<Transform>();
    [SerializeField]private string chooseUid;
    public  PackageCell currentChooseCell;
    [SerializeField] private Transform btnTran;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Button disequipBtn;
    [SerializeField] private Button deleteBtn;
    [SerializeField] private Button cancelBtn;
    public string ChooseUid
    {
        get { return chooseUid; }
        set
        {
            chooseUid = value;
        }
    }
    public int maxNum;
    private void Awake()
    {
        InitPackageItemParent(maxNum);
        btnTran = transform.Find("Btn");
        equipBtn = transform.Find("Btn/EquipBtn").GetComponent<Button>();
        /*disequipBtn = transform.Find("Btn/disequipBtn").GetComponent<Button>();*/
        deleteBtn = transform.Find("Btn/DeleteBtn").GetComponent<Button>();
        cancelBtn = transform.Find("Btn/CancelBtn").GetComponent<Button>();
    }

    void Start()
    {
        Refresh();
        equipBtn.onClick.AddListener(OnEquipBtn);
        disequipBtn.onClick.AddListener(OnDisequipBtn);
        deleteBtn.onClick.AddListener(OnDeleteBtn);
        cancelBtn.onClick.AddListener(OnCancelBtn);
    }

    public void ShowButton(bool active)
    {
        if (active)
        {
            btnTran.gameObject.SetActive(true);
            equipBtn.gameObject.SetActive(true);
            disequipBtn.gameObject.SetActive(false);
            //装备按钮设置
            PackageLocalItem localItem = PackageManager.Instance.GetPackageLocalItemByUId(chooseUid);
            if (localItem != null && localItem.isEquip != 0)
            {
                disequipBtn.gameObject.SetActive(true);
                equipBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            btnTran.gameObject.SetActive(false);
        }
    }
    private void OnDisequipBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Pick_Weapon);
        if (chooseUid == null || currentChooseCell == null)
        {
            return;
        }
        PackageLocalItem localItem = PackageManager.Instance.GetPackageLocalItemByUId(chooseUid);
        PlayerController.Instance.playerWeapon.RemoveGun(localItem);
        Refresh();
    }

    public void OnCancelBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        ShowButton(false);
        chooseUid = null;
        currentChooseCell.Select(false);
        currentChooseCell = null;
        
    }
    
    private void OnDeleteBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        PackageLocalItem localItem = PackageManager.Instance.GetPackageLocalItemByUId(chooseUid);
        if(localItem.isEquip != 0)
        {
            return;
        }
        ShowButton(false);
        currentChooseCell = null;
        DeleteItem();
    }

    private void OnEquipBtn()
    {
        if (chooseUid == null || currentChooseCell == null)
        {
            return;
        }
        SoundManager.Instance.PlaySound(SoundConst.Pick_Weapon);
        PackageLocalItem localItem = PackageManager.Instance.GetPackageLocalItemByUId(chooseUid);
        bool isEquip = PlayerController.Instance.playerWeapon.EquipGun(localItem);
        if (isEquip)
        {
            Refresh();
        }
        else
        {
            //提示不能装备
        }
    }
    public void ChangeChooce(PackageCell packageCell)
    {
        if (currentChooseCell != null)
        {
            currentChooseCell.Select(false);
        }
        currentChooseCell = packageCell;
        ShowButton(true);
        btnTran.transform.position = packageCell.transform.position + new Vector3(53.5f, 0, 0);
    }
    void InitPackageItemParent(int num)
    {
        Debug.Log("InitPackageItemParent");
        for(int i = 0;i < num;i ++)
        {
            GameObject PackageItemParent = Instantiate(PackageItemParentPrefab);
            PackageItemParent.transform.SetParent(Grid.transform, false);
            PackageItemParent.name = "ItemParent" + i.ToString();
            PackageItemParentsTransform.Add(PackageItemParent.transform);
        }
    }
    void Refresh()
    {
        chooseUid = null;
        currentChooseCell = null;
        RefreshGrid();
        ShowButton(false);
    }
    
    public void DeleteItem()
    {
        PackageManager.Instance.DeletePackageItem(chooseUid);
        chooseUid = null;
        Refresh();
    }
    void RefreshGrid()
    {
        foreach(Transform transform in PackageItemParentsTransform)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
        int i = 0;
        Debug.Log(PackageManager.Instance.GetSortPackageLocalData().Count);
        foreach (PackageLocalItem localItem in PackageManager.Instance.GetSortPackageLocalData())
        {
            Transform packageItem = Instantiate(PackageItemPrefab.transform, PackageItemParentsTransform[i]);
            PackageCell packageCell = packageItem.GetComponent<PackageCell>();
            packageCell.Refresh(localItem,this);
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
