using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDuration;
    [SerializeField] private Image sign;
    private void Awake()
    {
        sign = GetComponent<Image>();
    }
    void Start()
    {
        Animation();

    }
    public void Animation()
    {
        sign.transform.DOMoveY(transform.position.y - moveDistance, moveDuration)
            .SetLoops(-1, LoopType.Yoyo)  // 无限循环，来回移动
            .SetEase(Ease.InOutSine);    // 平滑的缓动效果
    }

}
