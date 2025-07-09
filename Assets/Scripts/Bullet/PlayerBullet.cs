using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBullet : Bullet
{
    //Éä³Ì
    public void OutOfDistance()
    {
        if (Vector2.Distance(transform.position, bornPos) > maxDistance)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagConst.Enemy))
        {
            other.GetComponent<Enemy>().TakeDamage(attack);
        }
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
        // Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }

}
