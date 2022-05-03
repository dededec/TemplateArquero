using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGuay : MonoBehaviour
{
    [SerializeField] public EconomyManager ec;
    
    public int amount = 0;
    // public List<Rewards> rewards;

    public void Start()
    {
        if(ec.Pay(amount))
        {
            Debug.Log("SA PAGAO PACO");
        }
    }
}
