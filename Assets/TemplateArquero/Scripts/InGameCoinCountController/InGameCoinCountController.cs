using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameCoinCountController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCountText;

    public void AddCoinAmount(int coinAmmount)
    {
        int coins = Int32.Parse(_coinCountText.text);
        coins += coinAmmount;
        _coinCountText.text = coins + "";
    }
}
