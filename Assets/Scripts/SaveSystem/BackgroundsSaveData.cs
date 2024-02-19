using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BackgroundsSaveData : SaveData
{
    public List<bool> UnlockedBackgrounds;
    public int CurrentBackground;
    public BackgroundsSaveData(List<bool> backgrounds, int current)
    {
        UnlockedBackgrounds = backgrounds;
        CurrentBackground = current;
    }
}
