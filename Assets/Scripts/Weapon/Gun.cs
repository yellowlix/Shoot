using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float interval;
    public int attack;
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    public Vector3 equipPos;
    public Vector3 equipScale;
    protected Transform muzzlePos;
    protected Transform shellPos;
    protected Vector2 mousePos;
    protected Vector2 direction;
    protected float timer;
    protected float flipX;
    protected float flipY;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
/*    protected InputController inputController;*/

    public virtual void Awake()
    {
        /*inputController = new InputController();*/
    }
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");
        flipX = transform.localScale.x;
        flipY = transform.localScale.y;
    }

/*    protected virtual void OnEnable()
    {
        inputController.Enable();
    }
    protected virtual void OnDisable()
    {
        inputController.Disable();
    }*/

    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-flipX, -flipY, 1);
        else
            transform.localScale = new Vector3(flipX, flipY, 1);
        Shoot();
    }

    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

        if (GameManager.Instance.inputController.Player.Fire.IsPressed())
        {
            if (timer == 0)
            {
                timer = interval;
                Fire();
            }
        }
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }
    }

    protected virtual void Fire()
    {
        animator.SetTrigger("Shoot");

        // GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bulletObject = ObjectPool.Instance.GetObject(bulletPrefab);
        bulletObject.transform.position = muzzlePos.position;

        float angel = Random.Range(-5f, 5f);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);
        bullet.attack = attack;
        // Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
