using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energy;
    [SerializeField] private Image energySlider;
    [SerializeField] private TextMeshProUGUI sc;
    [SerializeField] private TextMeshProUGUI hc;

    private float energyFull;

    private void Awake() {
        energyFull = energySlider.rectTransform.sizeDelta.x;
    }

    private void OnEnable()
    {
        EconomyManager.onValuesChanged += valuesChanged;
        valuesChanged();
    }

    private void OnDisable() {
        EconomyManager.onValuesChanged -= valuesChanged;
    }

    void valuesChanged()
    {
        energy.text = EconomyManager.Energy + "/20";
        energySlider.rectTransform.sizeDelta = new Vector2(EconomyManager.Energy >= 20 ? energyFull : (EconomyManager.Energy * energyFull) / 20, energySlider.rectTransform.sizeDelta.y);    
        sc.text = EconomyManager.SoftCoins + "";
        hc.text = EconomyManager.HardCoins + "";
    }

}
