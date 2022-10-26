using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 
/// </summary>
public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] LootsPools;//凋落物对象池
    static Dictionary<GameObject, Pool> dictionary;//对象池字典，包含所有对象池
    private void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(LootsPools);
    }
#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(LootsPools);
    }
#endif
    private void CheckPoolSize(Pool[] pools)//检查对象池的大小
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool:{0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }
    private void Initialize(Pool[] pools)//初始化对象池，加入字典
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools Prefab:" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;//创建新的空对象作为父物体
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }
    public static GameObject Release(GameObject prefab)//直接在对象池拿出一个对象，不设置对象属性
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();
    }
    public static GameObject Release(GameObject prefab, Vector3 position)//直接在对象池拿出一个对象，设置对象位置
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)//直接在对象池拿出一个对象，设置对象位置和旋转
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)//直接在对象池拿出一个对象，设置对象位置、旋转和缩放
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager could NOT find prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation, localScale);
    }
}
