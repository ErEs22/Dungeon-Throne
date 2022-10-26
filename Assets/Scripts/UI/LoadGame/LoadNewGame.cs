using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewGame : MonoBehaviour
{
    public void SetNewGameIndex()
    {
        SaveSystem.dataIndex = GameManager.Instance.CheckSaveDataFileCount() + 1;
    }
}
