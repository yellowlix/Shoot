using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobin : Enemy
{
    public float stopDistance;
    protected override void MoveToPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > stopDistance)
        {
            base.MoveToPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public override void OnChildTriggerEnter(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            if (!attacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }
    public override void OnChildTriggerExit(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            StopCoroutine(AttackPlayer());  // 玩家离开后停止攻击
            attacking = false;
        }
    }
    private IEnumerator AttackPlayer()
    {
        attacking = true;
        while (attacking)
        {
            PlayerController.Instance.playerHealth.Hurted(attack);
            yield return new WaitForSeconds(attackInterval);
        }
    }

}
