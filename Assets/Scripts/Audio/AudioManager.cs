using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource backgroundAudioSource;

    [SerializeField] AudioSource effectAudioSource;

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioData">音频对象</param>
    public void PlayBackgroundAudio(AudioData audioData)
    {
        backgroundAudioSource.volume = audioData.volume;
        backgroundAudioSource.clip = audioData.clip;
        backgroundAudioSource.Play();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioData">音频对象</param>
    public void PlayEffectAudio(AudioData audioData)
    {
        effectAudioSource.volume = audioData.volume;
        effectAudioSource.PlayOneShot(audioData.clip);
    }
}
