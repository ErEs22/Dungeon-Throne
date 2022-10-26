using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfo : MonoBehaviour
{
    [DisplayOnly] public int loadDataIndex = 1;

    public GameObject parent;

    public void SetLoadIndex()
    {
        SaveSystem.dataIndex = loadDataIndex;
    }
}
