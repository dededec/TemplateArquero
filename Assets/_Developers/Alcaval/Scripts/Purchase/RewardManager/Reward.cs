using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    #region Fields
 
    public string idItemRewarded;
    public int amount;
        
    #endregion

    public Reward(string id, int amount)
    {
        this.idItemRewarded = id;
        this.amount = amount;
    }

    public Reward()
    {
        this.idItemRewarded = "";
        this.amount = 0;
    }
}
