using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public void GiveReward(List<Reward> rewards)
    {
        foreach(Reward r in rewards)
        {
            Debug.Log(r.RewardedItem.name + " " + r.ammount);
            switch(r.RewardedItem.typeOfReward)
            {
                case Item.TypeOfReward.HARDCOIN:
                    EconomyManager.Add(EconomyManager.CoinType.HARDCOIN, r.ammount);
                    break;
                case Item.TypeOfReward.SOFTCOIN:
                    EconomyManager.Add(EconomyManager.CoinType.SOFTCOIN, r.ammount);
                    break;
                case Item.TypeOfReward.ENERGY:
                    EconomyManager.Add(EconomyManager.CoinType.ENERGY, r.ammount);
                    break;
                case Item.TypeOfReward.EQUIPMENT:
                    // Añadir tal objeto al inventario
                    break;
            }
        }

        // TODO Mostrar en pantalla lo que se ha conseguido
    }
}
