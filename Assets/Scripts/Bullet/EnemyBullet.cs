using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Player))
        {
            col.GetComponent<PlayerHealth>().Hurted(attack);
        }
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
