using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wallet
{
    public static int CurrentMoney { get; private set; }

    public static event Action MoneyChanged;

    public static void SetStartMoney()
    {
        CurrentMoney = SaveSystem.LoadData<MoneySaveData>().Money;
    }

    public static void AddMoney(int count)
    {
        CurrentMoney += count;
        var money = new MoneySaveData(CurrentMoney);
        SaveSystem.SaveData(money);
        MoneyChanged?.Invoke();
    }

    public static void RemoveMoney(int count)
    {
        CurrentMoney -= count;
        var money = new MoneySaveData(CurrentMoney);
        SaveSystem.SaveData(money);
        MoneyChanged?.Invoke();
    }

    public static bool CanRemoveMoney(int count)
    {
        if (CurrentMoney >= count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
