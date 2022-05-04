using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum TypeOfReward
    {
        HARDCOIN,
        SOFTCOIN,
        ENERGY,
        EQUIPMENT,
        
    }

    public enum Rarity
    {
        RARE,
        COMMON,
        COIN,
    }

    public int id;
    public TypeOfReward typeOfReward;
    public Rarity rarity;
    public new string name;
    public string description;
    public Sprite icon;
}
