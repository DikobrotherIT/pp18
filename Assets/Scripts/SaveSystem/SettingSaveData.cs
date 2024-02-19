using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingSaveData : SaveData
{
    public bool IsMusicOn { get; set; }
    public bool IsSoundOn { get; set; }

    public SettingSaveData (bool isMusic, bool isSound)
    {
        IsMusicOn = isMusic;
        IsSoundOn = isSound;
    }
}
