using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurrency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TemplateArquero.HardCoinManager.Instance.Add(10);
        Debug.Log(TemplateArquero.HardCoinManager.HardCoins);
    }
}
