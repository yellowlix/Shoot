using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Sprite orignalSprite;
    [SerializeField] private Sprite laterSprite;
    private Vector2 mousePos;
    public SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       /* Cursor.visible = false;*/
        /*Cursor.lockState = CursorLockMode.Confined;*/
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePos;
        if (GameManager.Instance.inputController.Player.Fire.IsPressed())
        {
            sp.sprite = laterSprite;
        }
        if (!GameManager.Instance.inputController.Player.Fire.IsPressed())
        {
            sp.sprite = orignalSprite;
        }
    }
}
