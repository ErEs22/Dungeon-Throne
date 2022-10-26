using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    /// <summary>
    /// 伤害量
    /// </summary>
    [DisplayOnly] public int damage = 10;

    [DisplayOnly] public Collider2D damageCollider;

    private void Awake()
    {
        InitilizeObject();
    }

    private void OnEnable()
    {
        InitilizeObject();
    }

    void InitilizeObject()
    {
        damageCollider = GetComponentInChildren<Collider2D>(true);
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果碰到的碰撞体实现了TakeDamage接口，则调用接口
        ICharacterBehaviour.ITakeDamage DamageCharacter;
        if (other.TryGetComponent<ICharacterBehaviour.ITakeDamage>(out DamageCharacter))
        {
            DamageCharacter.TakeDamage(damage);
        }
    }
}
