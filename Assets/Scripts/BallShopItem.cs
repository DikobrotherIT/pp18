using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShopItem : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private Button _buyButton;
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _soldPanel;
    public Action BallBought;

    private void Awake()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    public void OnBuyButtonClicked()
    {
        if (Wallet.CanRemoveMoney(_cost))
        {
            var balls = SaveSystem.LoadData<BallsSaveData>();
            balls.UnlockedBalls[_index] = true;
            SaveSystem.SaveData(balls);
            Wallet.RemoveMoney(_cost);
            BallBought?.Invoke();
        }
    }

    public void LockItem()
    {
        _buyButton.interactable = false;
        _soldPanel.SetActive(true);
    }

    public void UnlockItem()
    {
        _buyButton.interactable = true;
        _soldPanel.SetActive(false);
    }
}
