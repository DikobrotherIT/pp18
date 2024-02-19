using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<BallShopItem> _ballShopItems;
    [SerializeField] private List<BackgroundShopItem> _backgroundShopItems;

    private void OnEnable()
    {
        foreach (var item in _ballShopItems)
        {
            item.BallBought += UpdateBallShop;
        }
        foreach (var item in _backgroundShopItems)
        {
            item.BackBought += UpdateBackShop;
        }
        UpdateBallShop();
        UpdateBackShop();
    }

    private void OnDisable()
    {
        foreach (var item in _ballShopItems)
        {
            item.BallBought -= UpdateBallShop;
        }
        foreach (var item in _backgroundShopItems)
        {
            item.BackBought -= UpdateBackShop;
        }
    }

    private void UpdateBallShop()
    {
        var balls = SaveSystem.LoadData<BallsSaveData>();
        for (int i = 0; i < _ballShopItems.Count; i++)
        {           
            if (balls.UnlockedBalls[i + 1])
            {
                _ballShopItems[i].LockItem();
            }
            else
            {
                _ballShopItems[i].UnlockItem();
            }
        }
    }

    private void UpdateBackShop()
    {
        var back = SaveSystem.LoadData<BackgroundsSaveData>();
        for (int i = 0; i < _backgroundShopItems.Count; i++)
        {
            if (back.UnlockedBackgrounds[i + 1])
            {
                _backgroundShopItems[i].LockItem();
            }
            else
            {
                _backgroundShopItems[i].UnlockItem();
            }
        }
    }
}
