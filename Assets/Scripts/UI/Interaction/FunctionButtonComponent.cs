using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButtonComponent : MonoBehaviour
{
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Resume()
    {
        EventManager.Instance.Resume();
    }

    public void OnResumeScaleTime()
    {
        EventManager.Instance.ResumeScaleTime();
    }
}
