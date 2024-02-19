using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundShopItem : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private Button _buyButton;
    [SerializeField] private int _cost;
    [SerializeField] private GameObject _soldPanel;
    public Action BackBought;

    private void Awake()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    public void OnBuyButtonClicked()
    {
        if (Wallet.CanRemoveMoney(_cost))
        {
            var back = SaveSystem.LoadData<BackgroundsSaveData>();
            back.UnlockedBackgrounds[_index] = true;
            SaveSystem.SaveData(back);
            Wallet.RemoveMoney(_cost);
            BackBought?.Invoke();
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
