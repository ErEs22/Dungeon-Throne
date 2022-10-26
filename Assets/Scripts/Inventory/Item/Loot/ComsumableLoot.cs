using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComsumableLoot : Loot
{
    [SerializeField] float dropUpForce = 5;

    [DisplayOnly, SerializeField] SpriteRenderer icon;

    /// <summary>
    /// 拾取的物品对象
    /// </summary>
    /// <returns></returns>
    public ComsumableItem comsumableLoot;

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
        icon.sprite = Resources.Load("Sprites/Comsumable/" + comsumableLoot.iconName, typeof(Sprite)) as Sprite;
    }

    public void SetSprite()
    {
        icon = GetComponent<SpriteRenderer>();
        icon.sprite = Resources.Load("Sprites/Comsumable/" + comsumableLoot.iconName, typeof(Sprite)) as Sprite;
    }

    private void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            //当物品掉落在地上后，禁用重力，开启触发器并将层级改为可与玩家交互的层级
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

    public override void Collect()
    {
        base.Collect();
        playerInventory?.AddComsumableItem(comsumableLoot);
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
