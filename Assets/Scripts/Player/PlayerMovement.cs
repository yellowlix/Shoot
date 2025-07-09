using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using Model;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour, IController
{
    private MoveModel moveModel;
    private float shadowInterval = 0.04f;
    private float shadowTimer;
    private Vector2 move;
    private Vector2 mousePos;
    private Animator animator;
    private float flipX;
    [Header("Dash")] [SerializeField] private GameObject shadowPrefab;
    //冲刺剩余时间
    private float dashTimeRemain;
    //是否处于冲刺状态
    private bool isDashing;
    //上一次冲刺时间点
    private float LastDash;
    private PlayerProperty playerProperty;
    private PlayerController playerController;
    public Image dashIcon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerProperty = GetComponent<PlayerProperty>();
        playerController = GetComponent<PlayerController>();
        dashIcon = transform.Find("Canvas/Dash/DashIcon").GetComponent <Image>();
    }

    void Start()
    {
        moveModel = this.GetModel<MoveModel>();
        flipX = transform.localScale.x;
        transform.position = Vector2.zero;
        /*guns[0].SetActive(true);*/
        LastDash = -moveModel.DashCoolDown.Value;
        dashIcon.fillAmount = 1f;
        GameManager.Instance.inputController.Player.Dash.performed += ReadyToDash;
        Debug.Log("速度" + moveModel.MoveSpeed.Value);
        Debug.Log("冲刺速度：" + moveModel.DashSpeed.Value);
        Debug.Log("冲刺冷却时间：" + moveModel.DashCoolDown.Value);
        
    }

    private void FixedUpdate()
    {
        Dash();
        if (!isDashing)
        {
            Move();
        }
    }

    void Update()
    {
        UpdateDashIcon();
    }

    public void UpgradeSpeed(float num)
    {
        this.SendCommand(new UpgradeMoveSpeedCommand(num));
    }

    public void UpgradeDashCoolDown(float num)
    {
        this.SendCommand(new UpgradeDashCoolDownCommand(num));
    }


    void ReadyToDash(InputAction.CallbackContext callback)
    {
        if (Time.time - LastDash >= moveModel.DashCoolDown.Value)
        {
            LastDash = Time.time;
            StartDash();
            Debug.Log("qqq");
        }
    }

    void StartDash()
    {
        dashTimeRemain = moveModel.DashTime.Value;
        isDashing = true;
        playerProperty.SetIntangible(true);
        SetDashIcon(0);
    }

    void Dash()
    {
        if (!isDashing) return;

        if (dashTimeRemain > 0f)
        {
            Vector2 direction;

            if (playerController.rb.velocity.sqrMagnitude < 0.01f)
            {
                // 如果玩家静止，根据面朝方向 dash
                direction = new Vector2(transform.localScale.x, 0).normalized;
            }
            else
            {
                // 如果玩家在移动，则按当前移动方向 dash
                direction = playerController.rb.velocity.normalized;
            }

            playerController.rb.velocity = direction * moveModel.DashSpeed.Value;
            dashTimeRemain -= Time.fixedDeltaTime;
            CreateShadowIfNeeded();
        }
        else
        {
            isDashing = false;
            playerProperty.SetIntangible(false); // 取消无敌状态
        }
    }

    void CreateShadowIfNeeded()
    {
        shadowTimer -= Time.deltaTime;
        if (shadowTimer <= 0f)
        {
            ObjectPool.Instance.GetObject(shadowPrefab);
            shadowTimer = shadowInterval;
        }
    }
    public void SetDashIcon(float value)
    {
        dashIcon.fillAmount = value;
    }
    public void UpdateDashIcon()
    {
        if(dashIcon.fillAmount < 1)
        {
            dashIcon.fillAmount += 1.0f / moveModel.DashCoolDown.Value * Time.deltaTime;
        }
        
    }
    void Move()
    {
        move = GameManager.Instance.inputController.Player.Move.ReadValue<Vector2>();

        playerController.rb.velocity = move.normalized * (moveModel.MoveSpeed.Value * Time.fixedDeltaTime);
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector2(flipX, transform.localScale.y);
            playerController.ReverseCanvas(true);
        }
        else
        {
            transform.localScale = new Vector2(-flipX, transform.localScale.y);
            playerController.ReverseCanvas(false);
        }

        if (move != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    private void OnDestroy()
    {
        moveModel = null;
    }
    /*private void OnDisable()
    {
        GameManager.Instance.inputController.Player.Dash.performed -= ReadyToDash;
    }*/

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}