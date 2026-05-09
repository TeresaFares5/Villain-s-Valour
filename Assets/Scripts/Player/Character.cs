using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public static int maxHp = 500 ;
    public int currentHp = 500;

    public int armour = 0;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    
    private bool isDead;
    
    [Header("Magnet")]
    public Magnet magnet;
    public float colliderSize;

    [Header("Manual Wand")]
    public float canUseManualWeapon = 0f;
    public GameObject buttonManualWeapon;

    [Header("Skulls")]
    public float canUseSkulls = 0f;
    Skulls skullController;
    public int skullCount = 0;
    private int oldSkullCount;

    [Header("Cheese Rush")]
    public GameObject remy;
    public int hasRemy;
    public int hasCheese;
    public GameObject cheeseRushHd;
    public int cheeseRush;
    public Animator animatorCheese;

    [Header("Triangles")]
    public int power;
    public int damage;
    public int wisedom;
    public int courage;
    public int hasSacredTriangle;
    public Animator animatorSacredTriangle;

    [Header("Eldest Wand")]
    public GameObject EldestWand;
    public int eldestWand;

    [Header("Nobody")]
    public int hasNobody;
    
    [Header("Strange Head")]
    public int hasStrangeHead;
    public Animator strangeHeadAnimator;

    [Header("Ice Shards")]
    public int hasIceShards;
    public GameObject iceShards;

    private void Awake()
    {
        int addedHealth = PlayerPrefs.GetInt("addedHealth");
        maxHp = maxHp + addedHealth;
        PlayerPrefs.SetInt("maxHp", maxHp);
        PlayerPrefs.Save();

        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
        magnet = FindObjectOfType<Magnet>();
        skullController = FindObjectOfType<Skulls>();
        skullCount += skullController.upgradeLevel;
    }

    private void Start()
    {
        Item item = FindObjectOfType<Item>();
        UpdateHpBar();
        hasRemy = 0;
        hasCheese = 0;
        oldSkullCount = 1;
    }

    private void Update()
    {
        if (skullController.usingSkulls)
        {
            if (skullCount > oldSkullCount)
            {
                skullController.upgradeLevel = skullCount;
                skullController.HandleUpgrade();
                oldSkullCount = skullCount;
            }
           
        }
        
        magnet.magnetCollider.radius += colliderSize;
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;

        if (hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }

        if (hasNobody == 1)
        {
            UpdateHpBar();
        }
        UpdateHpBar();
       
        if (hasRemy >= 1)
        {
            remy.SetActive(true);
        }

        if(cheeseRush == 1)
        {
            animatorCheese.SetTrigger("CheeseRush");
            cheeseRush++;
        }

        if (hasSacredTriangle == 1)
        {
            UseSacredTriangle();
            hasSacredTriangle++;
        }
        if (hasStrangeHead == 1)
        {
            strangeHeadAnimator.SetTrigger("StrangeHead");
        }


        if (eldestWand == 1)
        {
            EldestWand.SetActive(true);
        }

        if(hasIceShards == 1)
        {
            iceShards.SetActive(true);
        }

    }

    public void UseSacredTriangle()
    {
        animatorSacredTriangle.SetTrigger("TriangleActivated");

        Invoke("UseSacredTriangle", 90);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) { return; }

        currentHp -= damage;

        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }

        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        
        int currentMaxHp = maxHp + armour; // Calculate current max HP with added armor
        hpBar.SetState(currentHp, currentMaxHp);
    }


    public void Heal(int amount)
    {
        if (currentHp <= 0) { return; }

        currentHp += amount;
        if (currentHp > maxHp + armour)
        {
            currentHp = maxHp + armour;
        }
    }


}
