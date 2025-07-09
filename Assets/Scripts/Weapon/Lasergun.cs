using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasergun : Gun
{
    private GameObject effect;
    private LineRenderer laser;
    private bool isShooting;
    private bool isAttacking;
    private float attackTimer = 0.2f;
    protected LayerMask cullingLayerMask;//排除层级
    protected Enemy currentEnemy;
    private Coroutine attackCoroutine;
    protected override void Start()
    {
        base.Start();
        laser = muzzlePos.GetComponent<LineRenderer>();
        effect = transform.Find("Effect").gameObject;
        cullingLayerMask = ~(1 << LayerMask.NameToLayer("Confiner") | 1 << LayerMask.NameToLayer("Interactable") | 1 << LayerMask.NameToLayer("Player"));
        currentEnemy = null;
        attackCoroutine = null;
    }

    protected override void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;
        if (GameManager.Instance.inputController.Player.Fire.WasPressedThisFrame())
        {
            SoundManager.Instance.PlaySound(SoundConst.LasergunSound);
        }

        if(GameManager.Instance.inputController.Player.Fire.IsPressed())
        {
            isShooting = true;
            laser.enabled = true;
            effect.SetActive(true);
        }
        if(!GameManager.Instance.inputController.Player.Fire.IsPressed())
        {
            isShooting = false;
            laser.enabled = false;
            effect.SetActive(false);
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            currentEnemy = null;
        }
        animator.SetBool("Shoot", isShooting);

        if (isShooting)
        {
            Fire();
        }
    }

    protected override void Fire()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(muzzlePos.position, direction, 150,cullingLayerMask);
        // Debug.DrawLine(muzzlePos.position, hit2D.point);
        laser.SetPosition(0, muzzlePos.position);
        laser.SetPosition(1, hit2D.point);
        if (hit2D.collider.CompareTag(TagConst.Enemy))
        {
            Enemy enemy = hit2D.collider.GetComponent<Enemy>();
            if(enemy != currentEnemy)
            {
                currentEnemy = enemy;
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                } 
                attackCoroutine = StartCoroutine(Attack(enemy));
            }
        }
        else
        {
            if(attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
        }
        effect.transform.position = hit2D.point;
        effect.transform.forward = -direction;
    }
    private IEnumerator Attack(Enemy enemy)
    {
        isAttacking = true;
        while (isAttacking)
        {
            enemy.TakeDamage(attack);
            yield return new WaitForSeconds(attackTimer);//？:先伤害后等待还是先等待后伤害
        }
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
}
