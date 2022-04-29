using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum Rarity
    {
        RARE,
        COMMON,
    }

    public int id;
    public Rarity rarity;
}
