using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rewardable : Interactable
{
    public float lerp = 5;
    public Transform playerTran;
    public bool isAttracted;
    private void Start()
    {
        playerTran = GameObject.FindWithTag(TagConst.Player).transform;
    }
    private void Update()
    {
        if (isAttracted)
        {
            Attracted();
        }
    }
    public virtual void Reward()
    {
    }
    public void SetAttracted(float moveTime)
    {
        isAttracted = true;
    }
    public void Attracted()
    {
        transform.position = Vector2.Lerp(transform.position, playerTran.position,lerp * Time.deltaTime);
    }
    public override void Interact()
    {
        
        Reward();
        base.Interact();
        ObjectPool.Instance.PushObject(gameObject);
    }
    void OnEnable()
    {
        if(playerTran == null) playerTran = GameObject.FindWithTag(TagConst.Player).transform;
        isAttracted = false;
    }
    private void OnDisable()
    {
        isAttracted = false;
    }
}
