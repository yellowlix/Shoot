using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class WeaponInteractable:Interactable
{
    public PackageLocalItem localItem;
    public Item item;
    public SpriteRenderer spriteRenderer;
    public Transform canvas;
    [SerializeField]private TMP_Text nameText;
    private bool canOperate;
    

    private void Awake()
    {
        
        canvas = transform.Find("Canvas");
        spriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        nameText = transform.Find("Canvas/ItemName").GetComponent<TMP_Text>();
        localItem = new PackageLocalItem();
    }
    private void Start()
    {
        canOperate = false;
        canvas.gameObject.SetActive(false);
    }
    public void Init(PackageLocalItem localItem)
    {
        this.localItem = localItem;
        item = PackageManager.Instance.GetItemById(this.localItem.id);
        spriteRenderer.sprite = item.icon;
        nameText.text = item.Name;
        nameText.color = Kit.ChooseColor(localItem.level);
    }
    private void Update()
    {
        if(canOperate)
        {
            if (GameManager.Instance.inputController.Player.EquipGun.WasPressedThisFrame())
            {
                //如果能装备的话直接装备
                SoundManager.Instance.PlaySound(SoundConst.Pick_Weapon);
                PlayerController.Instance.playerWeapon.EquipGun(localItem);
                Destroy(gameObject);
            }
        }
    }
    public override void Interact()
    {
        canOperate = true;
        canvas.gameObject.SetActive(true);
        /*base.Interact();
        if (GameManager.Instance.inputController.Player.EquipGun.WasPressedThisFrame())
        {
            //如果能装备的话直接装备
            PlayerController.Instance.playerWeapon.EquipGun(localItem);
            Destroy(gameObject);
        }*/
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            canOperate = false;
            canvas.gameObject.SetActive(false);
        }
    }
}

