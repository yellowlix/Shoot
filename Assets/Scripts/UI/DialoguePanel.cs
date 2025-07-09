using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    private void Awake()
    {
        dialogueText = transform.Find("Text").GetComponent<TMP_Text>();
    }
    public void ShowDialogue(string dialogue)
    {
        this.dialogueText.text = dialogue;
    }
}
