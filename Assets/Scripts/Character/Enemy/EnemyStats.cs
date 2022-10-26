using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, ICharacterBehaviour.ITakeDamage
{
    [SerializeField] Enemy_SO enemy_SO;

    EnemyManager EM;

    Animator animator;

    EnemyHealthBar healthBar;

    [DisplayOnly] public int baseMaxHealth;

    [DisplayOnly] public int currentMaxHealth;

    [DisplayOnly] public int currentHealth;

    [DisplayOnly] public int currentATK;

    [DisplayOnly] public bool Dead = false;

    private void Awake()
    {
        InitilizeObject();
        InitializeData();
    }

    private void OnEnable()
    {
        InitializeData();
    }

    void InitilizeObject()
    {
        animator = GetComponentInChildren<Animator>();
        EM = GetComponent<EnemyManager>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void InitializeData()
    {
        baseMaxHealth = enemy_SO.baseMaxHealth;
        currentMaxHealth = baseMaxHealth;
        currentHealth = currentMaxHealth;
        currentATK = enemy_SO.baseATK;
    }

    /// <summary>
    /// 受到攻击，根据伤害值减少血量
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EM.enemySFXHandler.PlayTakeHitSFX();
        //如果血量小于零，则敌人死亡，血量为零，进入死亡状态
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            EM.enemyStateMachine.ChangeState(eEnemyState.Death);
            EM.cameraHandler.ShakeCamera();
        }
        else
        {
            EM.enemyStateMachine.ChangeState(eEnemyState.Hit);
            EM.cameraHandler.ShakeCamera();
        }
        healthBar.UpdateStateBar(currentHealth, currentMaxHealth);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public void DieLater()
    {
        Invoke(nameof(Death), 3f);
    }

}
