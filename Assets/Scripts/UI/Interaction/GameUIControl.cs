using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIControl : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void OnEnable()
    {
        EventManager.Instance.pause += OnPause;
        EventManager.Instance.resume += OnResume;
        EventManager.Instance.resumeScaleTime += OnResumeScaleTime;
    }

    private void OnDisable()
    {
        EventManager.Instance.pause -= OnPause;
        EventManager.Instance.resume -= OnResume;
        EventManager.Instance.resumeScaleTime -= OnResumeScaleTime;
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        EventManager.Instance.DisableAllInput();
        EventManager.Instance.EnableGamePauseInput();
    }
    public void OnResume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        EventManager.Instance.EnableAllInput();
        EventManager.Instance.DisableGamePauseInput();
    }

    public void OnResumeScaleTime()
    {
        Time.timeScale = 1f;
    }
}
