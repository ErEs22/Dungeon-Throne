using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("---Movement---")]
    [SerializeField] float acceleration = 0.1f;

    [SerializeField] float deceleration = 0.1f;

    [SerializeField] float rollForce = 5f;

    [SerializeField] Transform model;

    [SerializeField] LayerMask rollingLayer;

    [SerializeField] LayerMask defaultLayer;

    public Rigidbody2D rb;

    PlayerManager PM;

    [DisplayOnly] public float velocityChangeDuration;

    private void Awake()
    {
        InitilizeObject();
    }

    private void Update()
    {
        if (!rb.IsAwake())
        {
            rb.WakeUp();
        }
    }

    /// <summary>
    /// 初始化类中的对象
    /// </summary>
    void InitilizeObject()
    {
        rb = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerManager>();
    }

    /// <summary>
    /// 处理角色移动
    /// </summary>
    /// <param name="speed">移动速度</param>
    public void Move(float speed)
    {
        SetVelocityX(HandleXMoveInput() * speed);
        model.localScale = new Vector3(-HandleXMoveInput(), 1, 1);
    }

    /// <summary>
    /// 处理X轴的输入值，防止输入值的绝对值小于1的情况下造成的影响
    /// </summary>
    /// <returns>处理后的值</returns>
    public float HandleXMoveInput()
    {
        if (PM.playerInputHandler.XMoveInput > 0)
        {
            return 1;
        }
        else if (PM.playerInputHandler.XMoveInput < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 处理翻滚
    /// </summary>
    public void HandleRoll()
    {
        ChangeToRollingLayer();
        if (PM.playerInputHandler.XMoveInput == 0)
        {
            rb.AddForce(new Vector2(rollForce * -model.localScale.x, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(rollForce * PM.playerInputHandler.XMoveInput, 0), ForceMode2D.Impulse);
        }
    }

    public void ChangeToRollingLayer()
    {
        gameObject.layer = (int)Mathf.Log(rollingLayer.value, 2);
    }

    public void ChangeToPlayerLayer()
    {
        gameObject.layer = (int)Mathf.Log(defaultLayer.value, 2);
    }

    /// <summary>
    /// 使角色停下
    /// </summary>
    public void StopPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    /// <summary>
    /// 设置X轴的刚体速度
    /// </summary>
    /// <param name="XAxis">X轴的速度值</param>
    void SetVelocityX(float XAxis)
    {
        if (AccelerateXVelocity(XAxis))
        {
            rb.velocity = new Vector2(XAxis, rb.velocity.y);
        }
    }

    /// <summary>
    /// X轴加速
    /// </summary>
    /// <param name="targetAmount"></param>
    /// <returns>是否加速完成</returns>
    public bool AccelerateXVelocity(float targetAmount)
    {
        if (velocityChangeDuration < acceleration)
        {
            velocityChangeDuration += Time.fixedDeltaTime;
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(targetAmount, rb.velocity.y), velocityChangeDuration / acceleration);
            return false;
        }
        return true;
    }

    /// <summary>
    /// X轴减速
    /// </summary>
    public void DecelerateXVelocity()
    {
        if (velocityChangeDuration < deceleration)
        {
            velocityChangeDuration += Time.fixedDeltaTime;
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, rb.velocity.y), velocityChangeDuration / deceleration);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
