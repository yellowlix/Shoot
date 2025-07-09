using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueText : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private Sequence sequence;
    private void Awake()
    {
        transform.localScale = Vector3.one;
        valueText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        Animation();
    }
    void Animation()
    {
        sequence = DOTween.Sequence(gameObject);
        sequence.Append(transform.DOMoveY(transform.position.y + 30, 0.5f))
            .Join(transform.DOScale(transform.localScale * 1.5f, 0.5f))
            .OnComplete(()=>UIManager.Instance.PushObject(gameObject));
    }
    public void SetValueText(string text,Color color)
    {
        valueText.text = text;
        valueText.color = color;
        Animation();
    }
}
