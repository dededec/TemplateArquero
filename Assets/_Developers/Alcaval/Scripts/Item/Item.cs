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
    public new string name;
    public string description;
    public Sprite icon;
    public Rarity rarity;
}
