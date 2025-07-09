using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Enemy
{
    public float attackRange = 3f;  // 怪物开始攻击的距离
    public float attackTimer = 0;
    public GameObject bulletPrefab;
    public Transform muzzlePos;
    private void Awake()
    {
        muzzlePos = transform.Find("muzzlePos");
    }


    protected override void MoveToPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position,player.transform.position);
        if(distanceToPlayer > attackRange)
        {
            base.MoveToPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }
    public void AttackPlayer()
    {
        rb.velocity = Vector3.zero;
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            ShootBullet();
            attackTimer = attackInterval;  // 重置射击冷却时间
        }
    }

    private void ShootBullet()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        GameObject bulletObject = ObjectPool.Instance.GetObject(bulletPrefab);
        bulletObject.transform.position = muzzlePos.position;
        float angel = Random.Range(-5f, 5f);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);
        bullet.attack = attack;
        Debug.Log("shoot");
    }
}
