using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelsSaveData : SaveData
{
    public List<bool> UnlockedLevels;
    public List<int> Stars;

    public LevelsSaveData(List<bool> levels, List<int> stars)
    {
        UnlockedLevels = levels;
        Stars = stars;
    }
}
