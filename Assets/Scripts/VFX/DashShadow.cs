using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    [Header("Timer Control")]
    //显示残影的时间
    public float activeTime;
    [Header("Opacity Control")]
    public float baseAlpha;
    //残影淡化速度
    public float fadeSpeed;
    private float alpha;
    private Color color;
    private SpriteRenderer dashSprite;
    private Transform player;
    private SpriteRenderer playerSprite;
    private float startTime;

    private void OnEnable()
    {
        player = GameObject.FindWithTag(TagConst.Player).transform;
        dashSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = baseAlpha;
        dashSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;
        startTime = Time.time;
    }
    void Update()
    {
        alpha -= fadeSpeed;
        color = new Color(0.3f, 0.7f, 1, alpha);
        if (color == null)
        {
            Debug.Log("11");
        }
        if (dashSprite == null)
        {
            Debug.Log("22");
        }
        dashSprite.color = color;
        if (Time.time - startTime >= activeTime)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
       
    }
}
