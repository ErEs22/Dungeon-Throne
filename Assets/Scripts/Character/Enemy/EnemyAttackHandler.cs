using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    EnemyManager EM;

    DamageCollider damageCollider;

    private void OnEnable()
    {
        InitializeObject();
        InitializeData();
    }

    void InitializeObject()
    {
        damageCollider = GetComponentInChildren<DamageCollider>();
        EM = GetComponentInParent<EnemyManager>();
    }

    void InitializeData()
    {
        damageCollider.damage = EM.enemyStats.currentATK;
    }

    public void EnableDamageCollider()
    {
        damageCollider.EnableDamageCollider();
    }

    public void DisableDamageCollider()
    {
        damageCollider.DisableDamageCollider();
    }
}
