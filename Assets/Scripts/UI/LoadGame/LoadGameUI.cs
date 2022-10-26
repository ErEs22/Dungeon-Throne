using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGameUI : MonoBehaviour
{
    [SerializeField] GameObject loadDataButton;

    private void OnEnable()
    {
        CreateLoadDataButton();
    }

    private void OnDisable()
    {
        DeleteLoadDataButton();
    }

    public void CreateLoadDataButton()
    {
        GameManager.Instance.saveDataFileCount = GameManager.Instance.CheckSaveDataFileCount();

        for (int i = 0; i < GameManager.Instance.saveDataFileCount; i++)
        {
            GameObject button = Instantiate(loadDataButton, Vector2.zero, Quaternion.identity, transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "存档" + (i + 1).ToString();
            button.GetComponentInChildren<LoadInfo>().loadDataIndex = i + 1;
        }
    }

    public void DeleteLoadDataButton()
    {
        LoadInfo[] objects = GetComponentsInChildren<LoadInfo>();
        foreach (var item in objects)
        {
            Destroy(item.parent);
        }
    }
}
