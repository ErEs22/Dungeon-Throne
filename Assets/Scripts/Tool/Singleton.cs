using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 泛型单例
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance
    {
        get;
        private set;
    }
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
