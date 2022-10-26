using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : Loot
{
    [SerializeField] float dropUpForce = 5;

    public WeaponItem weaponLoot;

    [DisplayOnly, SerializeField] SpriteRenderer icon;

    PlayerInventory playerInventory;

    PlayerInventory outPlayerInventory;

    Rigidbody2D rb;

    new BoxCollider2D collider2D;

    private void OnEnable()
    {
        InitializeObject();
        rb.AddForce(Vector2.up * dropUpForce, ForceMode2D.Impulse);
        rb.gravityScale = 3;
        collider2D.isTrigger = false;
        gameObject.layer = 3;
        icon = GetComponent<SpriteRenderer>();
        icon.sprite = Resources.Load("Sprites/Weapon/" + weaponLoot.iconName, typeof(Sprite)) as Sprite;
    }

    public void SetSprite()
    {
        icon = GetComponent<SpriteRenderer>();
        icon.sprite = Resources.Load("Sprites/Weapon/" + weaponLoot.iconName, typeof(Sprite)) as Sprite;
    }

    private void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            rb.gravityScale = 0;
            collider2D.isTrigger = true;
            gameObject.layer = 9;
        }
    }

    void InitializeObject()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        playerInventory = FindObjectOfType<PlayerInventory>(true);
    }

    public void Initialize(int level)
    {
        weaponLoot.level = level;
    }

    public override void Collect()
    {
        base.Collect();
        playerInventory?.AddWeaponItem(weaponLoot);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInventory>(out outPlayerInventory))
        {
            playerInventory.currentCollectable = this;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInventory>(out outPlayerInventory))
        {
            playerInventory.currentCollectable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInventory>(out outPlayerInventory))
        {
            playerInventory.currentCollectable = null;
        }
    }
}
