using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PurchaseProductDisplay : MonoBehaviour
{
    [SerializeField] private PurchaseProduct product;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;

    private void Start() {

        icon.sprite = product.icon;
        quantity.text = "" + product.quantity;
        price.text = product.price.ToString();
        description.text = product.description;

    }
    
}
