using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    GameObject currentWeaponModel;

    void UnLoadWeapon()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeapon(WeaponItem loadWeapon)
    {
        print("Load Weapon");
        UnLoadWeapon();

        if (Resources.Load("Prefabs/Weapon/Player/" + loadWeapon.iconName) == null)
        {
            return;
        }
        GameObject weapon = Instantiate(Resources.Load("Prefabs/Weapon/Player/" + loadWeapon.iconName)) as GameObject;
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
        currentWeaponModel = weapon;
    }
}
