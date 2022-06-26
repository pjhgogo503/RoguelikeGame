using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public void init()
    {
        TextAsset textasset = Managers.Resource.Load<TextAsset>($"Data/statData");
        Debug.Log($"{textasset.text}");
    }
}
