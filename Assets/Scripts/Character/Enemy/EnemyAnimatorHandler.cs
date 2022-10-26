using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAttackHandler))]
[RequireComponent(typeof(EnemyDropHandler))]
public class EnemyAnimatorHandler : MonoBehaviour
{
    public float currentAnimationLength;

    public string enterAnimName_Idle = "Idle";

    public string enterAnimName_Guard = "Move";

    public string enterAnimName_Chase = "Move";

    public string enterAnimName_Attack = "Attack";

    public string enterAnimName_Hit = "Hit";

    public string enterAnimName_Death = "Death";

    [HideInInspector] public Animator animator;

    private void Awake()
    {
        InitializeObject();
    }

    private void Update()
    {
        currentAnimationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
    }

    void InitializeObject()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 使用动画参数播放参数条件动画（Bool）
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="value">参数值</param>
    public void PlayTargetAnimationByParameter(string parameterName, bool value)
    {
        animator.SetBool(parameterName, value);
    }

    /// <summary>
    /// 使用动画参数播放参数条件动画（Trigger）
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    public void PlayTargetAnimationByParameter(string parameterName)
    {
        animator.SetTrigger(parameterName);
    }

    /// <summary>
    /// 更新动画器速度向量参数
    /// </summary>
    /// <param name="x">x方向</param>
    /// <param name="y">y方向</param>
    public void UpdateAnimatorMoveParameter(float x, float y)
    {
        if (Mathf.Abs(x) > 0)
        {
            x = 1;
        }
        else
        {
            x = 0;
        }
        animator.SetFloat("VelocityMagnitude", x);
    }
}
