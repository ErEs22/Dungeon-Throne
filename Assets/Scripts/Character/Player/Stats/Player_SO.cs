using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character/Player", fileName = "Player_SO")]
public class Player_SO : Character_SO
{
    public int baseMaxStamina;

    public int baseLevel;

    public float staminaRecoverTime = 1f;
}
