using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject explosionPrefab;
    new private Rigidbody2D rigidbody;
    public int attack;
    public Vector2 bornPos;
    public float maxDistance;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {

    }
/*    //射程
    public void OutOfDistance()
    {
        if(Vector2.Distance(transform.position,bornPos) > maxDistance)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }*/
/*    private void OnTriggerEnter2D(Collider2D other)
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
    }*/

}
