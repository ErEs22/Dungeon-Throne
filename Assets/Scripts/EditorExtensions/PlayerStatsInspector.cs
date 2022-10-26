using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsInspector : Editor
{
    PlayerStats playerStats;//自定义编辑器的对象初始化

    bool showDebugInfo = true;//true则显示调试信息

    private void OnEnable()
    {
        playerStats = (PlayerStats)target;//设置监视的对象
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        showDebugInfo = EditorGUILayout.Foldout(showDebugInfo, "Dubug Info");//折叠GUI，根据是否打开折叠GUI来返回Bool值
        if (showDebugInfo)//如果打开了折叠GUI，则渲染次对象
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }
#else
            if(!Application.isPlaying){
                return;
            }
#endif
            GUI.enabled = false;//false属性不能编辑，true属性能编辑
            //渲染的次对象
            EditorGUILayout.IntField("currentMaxHealth", playerStats.currentMaxHealth);
            EditorGUILayout.IntField("currentHealth", playerStats.currentHealth);
            EditorGUILayout.IntField("baseMaxHealth", playerStats.baseMaxHealth);
            EditorGUILayout.IntField("currentMaxStamina", playerStats.currentMaxStamina);
            EditorGUILayout.IntField("currentStamina", playerStats.currentStamina);
            EditorGUILayout.IntField("baseMaxStamina", playerStats.baseMaxStamina);
            EditorGUILayout.IntField("currentLevel", playerStats.currentLevel);
            EditorGUILayout.FloatField("staminaRecoverTime", playerStats.staminaRecoverTime);
            GUI.enabled = true;
        }
    }
}
