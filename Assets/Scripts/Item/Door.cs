using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using QFramework;
using UnityEngine;

public class Door : MonoBehaviour,IController
{
    private WeaponModel weaponModel;
    public bool isOpen;
    public string lockDialogue;
    public string exitDialogue;
    public Animator animator;
    public DialoguePanel dialoguePanel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialoguePanel = transform.Find("Canvas/DialoguePanel").GetComponent<DialoguePanel>();
    }
    void Start()
    {
        weaponModel = this.GetModel<WeaponModel>();
        isOpen = false;
        dialoguePanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        weaponModel = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            if(!isOpen)
            {
                
                if(weaponModel.EquipGuns.Count <= 0)
                {
                    dialoguePanel.gameObject.SetActive(true);
                    dialoguePanel.ShowDialogue(lockDialogue);
                }
                else
                {
                    StartCoroutine(OpenCoroutine(true));
                }
                
            }
            Invoke("CloseDialoguePanel", 2f);
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player) && isOpen)
        {
            SceneController.Instance.NewGameFirstScene();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            dialoguePanel.gameObject.SetActive(true);
            dialoguePanel.ShowDialogue(exitDialogue);
            if (isOpen)
            {
                animator.SetBool("isOpen", false);
                isOpen = false;
            }
            Invoke("CloseDialoguePanel", 2f);
        }
    }
    void CloseDialoguePanel()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
    IEnumerator OpenCoroutine(bool active)
    {
        animator.SetBool("isOpen", active);
        yield return new WaitForSeconds(1);
        isOpen = active;
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
