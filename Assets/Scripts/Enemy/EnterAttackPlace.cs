using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAttackPlace : MonoBehaviour
{
    private Enemy parent;
    private void Awake()
    {
        parent = transform.GetComponentInParent<Enemy>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        parent.OnChildTriggerEnter(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        parent.OnChildTriggerExit(col);
    }
}
