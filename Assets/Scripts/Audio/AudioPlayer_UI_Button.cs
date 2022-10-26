using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer_UI_Button : MonoBehaviour
{
    [SerializeField] AudioData select;

    [SerializeField] AudioData pressed;

    public void PlaySelectAudio()
    {
        AudioManager.Instance.PlayEffectAudio(select);
    }

    public void PlayPressedAudio()
    {
        AudioManager.Instance.PlayEffectAudio(pressed);
    }
}
