using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorBool : StateMachineBehaviour
{
    [Header("---Enter---")]
    [SerializeField] bool changeEnterBool = false;

    [SerializeField] string enterBoolName;

    [SerializeField] bool enterBoolValue;

    [Header("---Exit---")]
    [SerializeField] bool changeExitBool = false;

    [SerializeField] string exitBoolName;

    [SerializeField] bool exitBoolValue;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (changeEnterBool)
        {
            animator.SetBool(enterBoolName, enterBoolValue);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (changeExitBool)
        {
            animator.SetBool(exitBoolName, exitBoolValue);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
