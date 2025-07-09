using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int bulletNum;
    public float bulletAngle = 15;

    protected override void Fire()
    {
        animator.SetTrigger("Shoot");
        SoundManager.Instance.PlaySound(SoundConst.ShotGunSound);
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bulletObject = ObjectPool.Instance.GetObject(bulletPrefab);
            bulletObject.transform.position = muzzlePos.position;
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bulletNum % 2 == 1)
            {
                bullet.SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction);
            }
            else
            {
                bullet.SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * direction);
            }
            bullet.attack = attack;
        }

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
