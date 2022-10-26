using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;

    [HideInInspector] public PlayerInputHandler playerInputHandler;

    [HideInInspector] public PlayerAnimatorHandler playerAnimatorHandler;

    [HideInInspector] public PlayerAttackHandler playerAttackHandler;

    [HideInInspector] public PlayerInventory playerInventory;

    [HideInInspector] public WeaponSlotManager weaponSlotManager;

    [HideInInspector] public PlayerStats playerStats;

    [HideInInspector] public PlayerStateMachine playerStateMachine;

    [HideInInspector] public SFXHandler sFXHandler;

    [HideInInspector] public CameraHandler cameraHandler;

    private void Awake()
    {
        InitilizeObject();
    }

    /// <summary>
    /// 初始化类中的对象
    /// </summary>
    void InitilizeObject()
    {
        playerController = GetComponent<PlayerController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        playerAttackHandler = GetComponent<PlayerAttackHandler>();
        playerInventory = GetComponent<PlayerInventory>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        playerStats = GetComponent<PlayerStats>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        sFXHandler = GetComponentInChildren<SFXHandler>();
        cameraHandler = GetComponentInChildren<CameraHandler>();
    }
}
