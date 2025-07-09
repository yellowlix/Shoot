using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnCross : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private Sequence sequence;
    [SerializeField] private Vector3 originalScale = new Vector3(0.9f,0.9f,0.9f);
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        transform.localScale = originalScale;
        sp.color = new Color(255, 0, 0, 255);
    }
    private void Start()
    {
    }
    private void OnEnable()
    {
        if(sp == null) sp = GetComponent<SpriteRenderer>();
        transform.localScale = originalScale;
        sp.color = new Color(255, 0, 0, 255);
        InitAnimation();
    }
    public void InitAnimation()
    {
        sequence = DOTween.Sequence(gameObject);
        sequence.Append(sp.DOFade(0f, 0.3f).SetLoops(2,LoopType.Yoyo))
            .Append(sp.DOFade(0f, 0.1f).SetLoops(4,LoopType.Yoyo))
            .OnComplete(() =>
            {
                ObjectPool.Instance.PushObject(gameObject);
            });
    }
    private void OnDisable()
    {
    }
}
