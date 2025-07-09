using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationPanel : BasePanel
{
    public Button returnBtn;
    private void Awake()
    {
        returnBtn = transform.Find("ReturnBtn").GetComponent<Button>();
    }
    void Start()
    {
        returnBtn.onClick.AddListener(OnReturnBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnReturnBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        ClosePanel();
    }
}
