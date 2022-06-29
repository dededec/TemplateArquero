using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager
{
    /*
    Los cofres dan un solo objeto.
    Probabilidad: https://levelskip.com/mobile/Archero-Chest-Drop-Rates-Farming-Guide
    */

    public enum ChestRarity
    {
        COMMON,
        RARE,
    }

    private static ChestManager _instance;
    public static ChestManager instance
    {
        get 
        {
            if(_instance == null)
                _instance = new ChestManager();
            return _instance;
        }
    }

    private List<Item> _commonItems, _greatItems, _rareItems, _epicItems;
    [SerializeField] private ItemDatabaseManager _itemDatabase;

    private ChestManager() 
    {
        _itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabaseManager").GetComponent<ItemDatabaseManager>();
        _commonItems = new List<Item>();
        _greatItems = new List<Item>();
        _rareItems = new List<Item>();
        _epicItems = new List<Item>();

        // Cargamos los items en las listas
        foreach(var item in _itemDatabase.GetAllItems().FindAll(item => item.typeOfReward == Item.TypeOfReward.EQUIPMENT))
        {
            switch(item.rarity)
            {
                case Item.Rarity.COMMON:
                _commonItems.Add(item);
                break;
                case Item.Rarity.RARE:
                _greatItems.Add(item);
                break;
                case Item.Rarity.GREAT:
                _rareItems.Add(item);
                break;
                case Item.Rarity.EPIC:
                _epicItems.Add(item);
                break;
            }
        }
    }

    public Item GenerateChest(ChestRarity rarity)
    {
        Item result = null;
        float prob = Random.Range(0f, 1f);
        switch(rarity)
        {
            case ChestRarity.COMMON:
            if(prob < 0.8)
            {
                // 80% prob. de common
                result = _commonItems[Random.Range(0, _commonItems.Count)];
            }
            else
            {
                // 20% prob. de great
                result = _greatItems[Random.Range(0, _greatItems.Count)];
            }
            break;
            
            case ChestRarity.RARE:
            if(prob < 0.50)
            {
                // 50% prob. de great
                result = _greatItems[Random.Range(0, _greatItems.Count)];
            }
            else if(prob < 0.93)
            {
                // 43% prob. de rare
                result = _rareItems[Random.Range(0, _rareItems.Count)];
            }
            else
            {
                // 7% prob. de epic
                result = _epicItems[Random.Range(0, _epicItems.Count)];
            }
            break;
        }

        return result;
    }
}
