using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] Image transitionImage;
    [SerializeField] float fadeTime = 3f;
    Color color;
    IEnumerator LoadCoroutine(string sceneName)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;
        transitionImage.gameObject.SetActive(true);
        while (color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;
            yield return null;
        }
        yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);
        loadingOperation.allowSceneActivation = true;
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;
            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
    }
    public void LoadGamePlayScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine("GamePlay"));
    }
    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine("MainMenu"));
    }
    public void LoadScoreRankScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine("ScoreRank"));
    }

    public void LoadSceneByName(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine(sceneName));
    }
}
