using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BallsSaveData : SaveData
{
    public List<bool> UnlockedBalls;
    public int CurrentBall;
    public BallsSaveData(List<bool> balls, int current)
    {
        UnlockedBalls = balls;
        CurrentBall = current;
    }
}
