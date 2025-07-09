using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : BasePanel
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Button returnBtn;
    private void Awake()
    {
        soundSlider = transform.Find("SoundSlider/Slider").GetComponent<Slider>();
        soundEffectSlider = transform.Find("SoundEffectSlider/Slider").GetComponent<Slider>();
        fullScreenToggle = transform.Find("FullScreen/Toggle").GetComponent<Toggle>();
        returnBtn = transform.Find("ReturnBtn").GetComponent<Button>();
    }
    void Start()
    {
        soundSlider.value = SoundManager.Instance.GetBackgroundMusicVolume();
        soundEffectSlider.value = SoundManager.Instance.GetSoundEffectVolume();
        fullScreenToggle.isOn = Screen.fullScreen;
        soundSlider.onValueChanged.AddListener(value => SoundManager.Instance.SetBackgroundMusicVolume(value));
        soundEffectSlider.onValueChanged.AddListener(value => SoundManager.Instance.SetSoundEffectVolume(value));
        fullScreenToggle.onValueChanged.AddListener(isFullScreen => SetFullScreen(isFullScreen));
        returnBtn.onClick.AddListener(OnReturnBtn);
    }

    private void OnReturnBtn()
    {
        SoundManager.Instance.PlaySound(SoundConst.Click);
        ClosePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
