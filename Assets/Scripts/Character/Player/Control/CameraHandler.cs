using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] ShakeData shakeData;

    public void ShakeCamera()
    {
        CameraShakerHandler.Shake(shakeData);
    }
}
