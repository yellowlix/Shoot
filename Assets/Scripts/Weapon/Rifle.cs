using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    protected LayerMask cullingLayerMask;//排除的层级
    protected override void Start()
    {
        base.Start();
        cullingLayerMask = ~(1 << LayerMask.NameToLayer("Confiner") | 1 << LayerMask.NameToLayer("Interactable") | 1 << LayerMask.NameToLayer("Player"));
    }
    protected override void Fire()
    {
        animator.SetTrigger("Shoot");

        RaycastHit2D hit2D = Physics2D.Raycast(muzzlePos.position, direction, 150,cullingLayerMask);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        LineRenderer tracer = bullet.GetComponent<LineRenderer>();
        tracer.SetPosition(0, muzzlePos.position);
        tracer.SetPosition(1, hit2D.point);
        Debug.Log("hit2D:" + hit2D.point);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}

