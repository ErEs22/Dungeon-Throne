using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频对象，包括音频片段和音量大小
/// </summary>
[System.Serializable]
public class AudioData
{
    [SerializeField] public AudioClip clip;

    [SerializeField][Range(0f, 1f)] public float volume = 0.5f;
}
