using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int interactableLayerMask;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void Start()
    {
        interactableLayerMask = LayerMask.GetMask("Interactable");//指定检测层级为交互层级
    }
    private void Update()
    {
        var others = Physics2D.OverlapCircleAll(transform.position, radius,interactableLayerMask);
        foreach(var col in others)
        {
            
            if (col.CompareTag(TagConst.Rewardable))
            {
                col.GetComponent<Rewardable>().Attracted();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagConst.Interactable) || col.CompareTag(TagConst.Rewardable))
        {
            col.GetComponent<Interactable>().Interact();
        }
    }
}
