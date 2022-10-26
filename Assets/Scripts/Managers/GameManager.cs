using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class GameManager : PersistentSingleton<GameManager>
{
    [DisplayOnly] public int saveDataFileCount = 0;

    protected override void Awake()
    {
        base.Awake();
        saveDataFileCount = CheckSaveDataFileCount();
    }

    private void OnEnable()
    {
        EventManager.Instance.onGameFinished += GameFinished;
    }

    private void OnDisable()
    {
        EventManager.Instance.onGameFinished -= GameFinished;
    }

    private void Start()
    {
        ItemManager.LoadWeaponItems();
        ItemManager.LoadEquipmentItems();
        ItemManager.LoadComsumableItems();
    }

    public int CheckSaveDataFileCount()
    {
        int count = 0;
        int index = 1;
        while (SaveSystem.SaveFileExists(SaveSystem.saveFileName + index + ".txt"))
        {
            index++;
            count++;
        }
        return count;
    }

    void GameFinished()
    {
        SceneLoader.Instance.LoadSceneByName("LevelSelect");
    }
}
