using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    PlayerManager PM;

    public WeaponSlot weaponSlot;

    private void Awake()
    {
        InitilizeObject();
    }

    void InitilizeObject()
    {
        weaponSlot = GetComponentInChildren<WeaponSlot>();
        PM = GetComponentInParent<PlayerManager>();
    }

    /// <summary>
    /// 加载武器到栏位上
    /// </summary>
    /// <param name="weapon">要加载的武器</param>
    public void LoadWeaponToSlot(WeaponItem weapon)
    {
        weaponSlot.LoadWeapon(weapon);
    }

    public void EnableDamageCollider()
    {
        if (PM.playerAttackHandler.currentWeapon == null)
        {
            return;
        }
        // weaponSlot.GetComponentInChildren<DamageCollider>().damage =
        // PM.playerAttackHandler.currentWeapon.baseATK * PM.playerAttackHandler.currentWeapon.level;
        weaponSlot.GetComponentInChildren<DamageCollider>().EnableDamageCollider();
    }

    public void DisableDamageCollider()
    {
        if (PM.playerAttackHandler.currentWeapon == null)
        {
            return;
        }
        weaponSlot.GetComponentInChildren<DamageCollider>().DisableDamageCollider();
    }
}
