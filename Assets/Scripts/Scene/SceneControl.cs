using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneLoader.Instance.LoadSceneByName(sceneName);
    }
}
