using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, PlayerInputActions.IGameUIActions, PlayerInputActions.IPlayerGameActionsActions, PlayerInputActions.IGamePauseActions
{
    PlayerInputActions playerInputActions;//输入系统生成的输入配置脚本

    public Vector2 MoveInput => playerInputActions.PlayerMovement.Move.ReadValue<Vector2>().normalized;//获取移动输入的二维变量

    public float XMoveInput => MoveInput.x;//获取移动输入的X轴的值

    public bool Moving => XMoveInput != 0;//X轴是否有输入，有则表示人物在移动

    public bool LightAttack => playerInputActions.PlayerAttack.LightAttack.IsPressed();//轻攻击按键是否按下

    public bool HeavyAttack => playerInputActions.PlayerAttack.HeavyAttack.IsPressed();//重攻击按键是否按下

    public bool Roll => playerInputActions.PlayerMovement.Roll.IsPressed();//翻滚按键是否按下

    public event UnityAction showInventory = delegate { };

    public event UnityAction collect = delegate { };

    public event UnityAction useComsumable = delegate { };

    private void Awake()
    {
        if (playerInputActions == null)
        {
            //如果输入配置脚本为空，则创建一个新的配置脚本对象
            playerInputActions = new PlayerInputActions();
            playerInputActions.PlayerMovement.Enable();
            playerInputActions.PlayerAttack.Enable();
            playerInputActions.GameUI.Enable();
            playerInputActions.PlayerGameActions.Enable();
            playerInputActions.GameUI.SetCallbacks(this);
            playerInputActions.PlayerGameActions.SetCallbacks(this);
            playerInputActions.GamePause.SetCallbacks(this);
        }
        EventManager.Instance.enableAllInput += EnableAllInput;
        EventManager.Instance.disableAllInput += DisableAllInput;
        EventManager.Instance.enableGamePauseInput += EnableGamePauseInput;
        EventManager.Instance.disableGamePauseInput += DisableGamePauseInput;
    }

    public void EnableGamePauseInput()
    {
        playerInputActions.GamePause.Enable();
    }

    public void DisableGamePauseInput()
    {
        playerInputActions.GamePause.Enable();
    }

    /// <summary>
    /// 禁用所有玩家输入控制
    /// </summary>
    public void DisableAllInput()
    {
        playerInputActions.Disable();
    }

    /// <summary>
    /// 启用所有玩家输入控制
    /// </summary>
    public void EnableAllInput()
    {
        playerInputActions.Enable();
    }

    public void DisablePlayerActions()
    {
        playerInputActions.PlayerAttack.Disable();
        playerInputActions.PlayerMovement.Disable();
    }

    public void EnablePlayerActions()
    {
        playerInputActions.PlayerAttack.Enable();
        playerInputActions.PlayerMovement.Enable();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            showInventory.Invoke();
        }
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            collect.Invoke();
        }
    }

    public void OnUseComsumable(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            useComsumable.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.Instance.Pause();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.Instance.Resume();
        }
    }
}
