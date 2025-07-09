using System.Collections;
using System.Collections.Generic;
using Command;
using QFramework;
using UnityEngine;

public class Enemy : MonoBehaviour,IController
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Vector2 flip;
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject goldCoinPrefab;
    [SerializeField] protected GameObject ExpStonePrefab;
    protected float hTimer;
    protected float hTime = 0.3f;

    public int maxHp;
    public int currentHp;
    public int attack;
    public float attackInterval;
    public bool attacking;
    public bool isDie;
    // Start is called before the first frame update
    void Start()
    {
        isDie = false;
        flip = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag(TagConst.Player);
    }
    protected virtual void OnEnable()
    {
        currentHp = maxHp;
        isDie = false;
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        if (player == null) player = GameObject.FindWithTag(TagConst.Player);
    }
    private void FixedUpdate()
    {
        FacePlayer();
        MoveToPlayer();
    }
    void Update()
    {
        if(hTimer > 0)
        {
            hTimer -= Time.deltaTime;
            if(hTimer <= 0)
            {
                spriteRenderer.color = Color.white;
                hTimer = 0;
            }
        }
    }
    
    protected virtual void FacePlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector2(-flip.x, flip.y);
        }
        else
        {
            transform.localScale = new Vector2(flip.x, flip.y);
        }
    }

    protected virtual void MoveToPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
        spriteRenderer.color = Color.red;
        GameManager.Instance.ShowValueText(damage.ToString(),transform.position, Color.red);
        hTimer = hTime;
        if(currentHp <= 0 && !isDie)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDie = true;
        GenerateGold();
        GenerateExpStone();
        ObjectPool.Instance.PushObject(this.gameObject);
        this.SendCommand(new EnemyDieCommand());
    }

    public virtual void OnChildTriggerEnter(Collider2D col)
    {
    }
    public virtual void OnChildTriggerExit(Collider2D col)
    {
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    protected virtual void GenerateGold()
    {
        GameObject goldCoin =  ObjectPool.Instance.GetObject(goldCoinPrefab);
        goldCoin.transform.position = new Vector2(transform.position.x - 0.1f,transform.position.y);
    }
    protected virtual void GenerateExpStone()
    {
        GameObject ExpStone = ObjectPool.Instance.GetObject(ExpStonePrefab);
        ExpStone.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
    }

    public IArchitecture GetArchitecture()
    {
        return GameArchitecture.Interface;
    }
}
