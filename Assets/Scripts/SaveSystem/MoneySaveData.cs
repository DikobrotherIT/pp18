using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MoneySaveData : SaveData
{
    public int Money;

    public MoneySaveData(int money)
    {
        Money = money;
    }
}
