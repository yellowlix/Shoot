using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject root = GameObject.Find("PlayerController");
                instance = root.GetComponent<PlayerController>();
                if (instance == null)
                {
                    instance = root.AddComponent<PlayerController>();
                }
            }
            return instance;
        }
    }
    public Collider2D col;
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public PlayerMovement playerMovement;
    public PlayerWeapon playerWeapon;
    public PlayerHealth playerHealth;
    public Transform canvas;
    public Image hpBar;
    public TMP_Text gunNameText;

    public float hTimer;
    public float hTime = 0.3f;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        col = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerHealth = GetComponent<PlayerHealth>();
        canvas = transform.Find("Canvas");
        hpBar = transform.Find("Canvas/HpBar/Hp").GetComponent<Image>();
        gunNameText = transform.Find("Canvas/GunName").GetComponent<TMP_Text>();

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        hpBar.fillAmount = 1f;

        gunNameText.gameObject.SetActive(false);
    }
    public void ReverseCanvas(bool reverse)
    {
        if (reverse)
        {
            canvas.localScale = Vector3.one * canvas.localScale.z; 
        }
        else
        {
            canvas.localScale = new Vector3(-1, 1, 1) * canvas.localScale.z;
        }
    }

    public void ShowGunName(string name, int level)
    {
        gunNameText.gameObject.SetActive(true); 
        gunNameText.color = Kit.ChooseColor(level);
        gunNameText.text = name;
        Invoke("HideGunNameText", 1.5f);
    }
    void HideGunNameText()
    {
        gunNameText.gameObject.SetActive(false);
    }
    public void SetHpBar(int currentHp,int maxHp)
    {
        float fill = (float)currentHp / maxHp;
        if (fill < 0.3f)
        {
            hpBar.color = Color.red;
        }
        else if (fill < 0.6f)
        {
            hpBar.color = Color.yellow;
        }
        else
        {
            hpBar.color= Color.green;
        }
        hpBar.fillAmount = fill;
    }
    // Update is called once per frame
    void Update()
    {
        if(hTimer > 0)
        {
            hTimer -= Time.deltaTime;
            if (hTimer <= 0)
            {
                sp.color = Color.white;
                hTimer = 0;
            }
        }
    }

    public void SetRed()
    {
        hTimer = hTime;
        sp.color = Color.red;
    }
}
