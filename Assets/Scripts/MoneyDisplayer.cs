using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        _moneyText.text = Wallet.CurrentMoney.ToString();
        Wallet.MoneyChanged += UpdateMoney;
    }

    private void OnDisable()
    {
        Wallet.MoneyChanged -= UpdateMoney;
    }

    public void UpdateMoney()
    {
        _moneyText.text = Wallet.CurrentMoney.ToString();
    }
}
