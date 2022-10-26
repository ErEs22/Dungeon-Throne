using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] AudioData lightAttackData;

    [SerializeField] AudioData heavyAttackData;

    [SerializeField] AudioData takeHitData;

    public void PlayLightAttackSFX()
    {
        AudioManager.Instance.PlayEffectAudio(lightAttackData);
    }

    public void PlayHeavyAttackSFX()
    {
        AudioManager.Instance.PlayEffectAudio(heavyAttackData);
    }

    public void PlayTakeHitSFX()
    {
        AudioManager.Instance.PlayEffectAudio(takeHitData);
    }
}
