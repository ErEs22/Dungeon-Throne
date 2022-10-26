using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXHandler : MonoBehaviour
{
    [SerializeField] AudioData attackData;

    [SerializeField] AudioData takeHitData;

    public void PlayAttackSFX()
    {
        AudioManager.Instance.PlayEffectAudio(attackData);
    }

    public void PlayTakeHitSFX()
    {
        AudioManager.Instance.PlayEffectAudio(takeHitData);
    }
}
