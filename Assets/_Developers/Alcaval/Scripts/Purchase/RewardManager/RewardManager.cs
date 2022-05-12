using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private ItemDatabaseManager _itemDatabaseManager;
    [SerializeField] private InventoryManager _inventoryManager;

    public void GiveReward(List<Reward> rewards)
    {
        foreach(Reward r in rewards)
        {
            Item RewardedItem = _itemDatabaseManager.GetItem(r.idItemRewarded);
            switch(RewardedItem.typeOfReward)
            {
                case Item.TypeOfReward.HARDCOIN:
                    EconomyManager.Add(EconomyManager.CoinType.HARDCOIN, r.ammount);
                    Debug.Log("Hardcoin rewarded");
                    break;
                case Item.TypeOfReward.SOFTCOIN:
                    EconomyManager.Add(EconomyManager.CoinType.SOFTCOIN, r.ammount);
                    Debug.Log("Softcoin rewarded");
                    break;
                case Item.TypeOfReward.ENERGY:
                    EconomyManager.Add(EconomyManager.CoinType.ENERGY, r.ammount);
                    Debug.Log("Energy rewarded");
                    break;
                case Item.TypeOfReward.EQUIPMENT:
                    // Añadir tal objeto al inventario por ejemplo un objeto random o algo
                    _inventoryManager.AddToInventory(r.idItemRewarded);
                    Debug.Log("Other rewarded");
                    break;
            }
        }

        // TODO Mostrar en pantalla lo que se ha conseguido
    }
}
