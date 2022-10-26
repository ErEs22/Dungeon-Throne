using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacterBehaviour.ITakeDamage
{
    [SerializeField] Player_SO player_SO;

    [DisplayOnly] public float recoverStaminaCountDown = 0;

    HealthBar healthBar;

    StaminaBar staminaBar;

    PlayerManager PM;

    [HideInInspector] public int currentMaxHealth;

    [HideInInspector] public int currentHealth;

    [HideInInspector] public int baseMaxHealth;

    [HideInInspector] public int currentMaxStamina;

    [HideInInspector] public int currentStamina;

    [HideInInspector] public int baseMaxStamina;

    [HideInInspector] public int currentLevel;

    [HideInInspector] public float staminaRecoverTime;

    public int currentLightATK => PM.playerAttackHandler.currentWeapon.baseATK * PM.playerAttackHandler.currentWeapon.level;

    public int currentHeavyATK => (int)(PM.playerAttackHandler.currentWeapon.baseATK * PM.playerAttackHandler.currentWeapon.baseStaminaHeavyMultiplier * PM.playerAttackHandler.currentWeapon.level);

    private void Awake()
    {
        InitilizeData();
        InitializeObject();
    }

    private void FixedUpdate()
    {
        RecoverStaminaWhenOutOfCombat();
    }

    void InitializeObject()
    {
        healthBar = FindObjectOfType<HealthBar>(true);
        staminaBar = FindObjectOfType<StaminaBar>(true);
        PM = GetComponent<PlayerManager>();
    }

    void InitilizeData()
    {
        currentMaxHealth = player_SO.baseMaxHealth;
        currentHealth = currentMaxHealth;
        baseMaxHealth = player_SO.baseMaxHealth;
        currentMaxStamina = player_SO.baseMaxStamina;
        currentStamina = currentMaxStamina;
        baseMaxStamina = player_SO.baseMaxStamina;
        currentLevel = player_SO.baseLevel;
        staminaRecoverTime = player_SO.staminaRecoverTime;
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PM.sFXHandler.PlayTakeHitSFX();
        //如果当前血量低于零，则设为零
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Death));
            PM.cameraHandler.ShakeCamera();
        }
        else
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Hit));
            PM.cameraHandler.ShakeCamera();
        }
        healthBar.UpdateStateBar(currentHealth, currentMaxHealth);
    }

    void Death()
    {
        PM.playerController.rb.simulated = false;
        SceneLoader.Instance.LoadSceneByName("LevelSelect");
    }

    public void DieLater()
    {
        Invoke(nameof(Death), 3f);
    }

    /// <summary>
    /// 恢复血量
    /// </summary>
    /// <param name="amount"></param>
    public void RecoverHealth(int amount)
    {
        float percent = amount * 0.01f;
        currentHealth += (int)(percent * currentMaxHealth);
        //如果血量增加之后大于最大值，则设为最大值
        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
        //更新状态条UI
        healthBar.UpdateStateBar(currentHealth, currentMaxHealth);
    }

    /// <summary>
    /// 消耗耐力
    /// </summary>
    /// <param name="cost"></param>
    public void CostStamina(int cost)
    {
        currentStamina -= cost;
        //如果当前耐力低于零，则设为零
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        staminaBar.UpdateStateBar(currentStamina, currentMaxStamina);
    }

    public void RecoverStamina(int amount)
    {
        currentStamina += amount;
        //如果耐力增加之后大于最大值，则设为最大值
        if (currentStamina > currentMaxStamina)
        {
            currentStamina = currentMaxStamina;
        }
        //更新状态条UI
        staminaBar.UpdateStateBar(currentStamina, currentMaxStamina);
    }

    /// <summary>
    /// 当脱离战斗之后持续恢复耐力值
    /// /// </summary>
    void RecoverStaminaWhenOutOfCombat()
    {
        recoverStaminaCountDown -= Time.deltaTime;
        if (recoverStaminaCountDown > 0)
        {
            return;
        }
        if (currentStamina >= currentMaxStamina)
        {
            return;
        }
        //在FixedUpdate中更新，每帧恢复1点
        RecoverStamina(1);
    }

    public void RecalculatePlayerStats()
    {
        float currentHealthPercent = (float)currentHealth / (float)currentMaxHealth;
        currentMaxHealth = baseMaxHealth + (currentLevel * 10) + PM.playerInventory.currentEquipment.baseGain * PM.playerInventory.currentEquipment.level;
        currentHealth = (int)(currentMaxHealth * currentHealthPercent);
    }
}
