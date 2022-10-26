using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInputHandler))]//自定义编辑器的具体对象
public class PlayerInputHandlerInspector : Editor
{
    PlayerInputHandler playerInputHandler;//自定义编辑器的对象初始化

    bool showDebugInfo = true;//true则显示调试信息

    private void OnEnable()
    {
        playerInputHandler = (PlayerInputHandler)target;//设置监视的对象
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
            EditorGUILayout.Vector2Field("MoveInput", playerInputHandler.MoveInput);
            EditorGUILayout.FloatField("XMoveInput", playerInputHandler.XMoveInput);
            EditorGUILayout.Toggle("Moving", playerInputHandler.Moving);
            EditorGUILayout.Toggle("LightAttack", playerInputHandler.LightAttack);
            EditorGUILayout.Toggle("HeavyAttack", playerInputHandler.HeavyAttack);
            EditorGUILayout.Toggle("Roll", playerInputHandler.Roll);
            GUI.enabled = true;
        }
    }
}
