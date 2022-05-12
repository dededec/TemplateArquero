using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// ! SHOULD BE A DONT DESTROY ON LOAD GAMEOBJECT
public class ItemDatabaseManager : MonoBehaviour
{
    // LIST OF EVERY ITEM IN THE GAME, LOADED BY A CSV
    [SerializeField] private List<Item> _EveryItemList;

    private void Awake() 
    {
        DontDestroyOnLoad(this);
        loadData();
    }

    public void loadData()
    {
        _EveryItemList.Clear();
        List<Dictionary<string, object>> data = CSVReader.Read("ItemDatabase");

        for(int i = 0; i < data.Count; i++)
        {
            string id = data[i]["id"].ToString();

            string itemName = data[i]["ItemName"].ToString();

            Item.TypeOfReward typeOfReward;
            Enum.TryParse(data[i]["typeOfReward"].ToString(), out typeOfReward);

            Item.InventoryUse inventoryUse;
            Enum.TryParse(data[i]["inventoryUse"].ToString(), out inventoryUse);

            Item.Rarity rarity;
            Enum.TryParse(data[i]["rarity"].ToString(), out rarity);

            string description = data[i]["description"].ToString();

            string iconPath = data[i]["icon"].ToString();
            Item item = ScriptableObject.CreateInstance<Item>();
            item.init(id, itemName, typeOfReward, inventoryUse, rarity, description, iconPath);
            _EveryItemList.Add(item);
        }
    }

    public Item GetItem(string id)
    {
        Item aux = null;
        foreach(Item i in _EveryItemList)
        {
            if(i.id == id)
            {
                aux = i;
                break;
            }
        }
        return aux;
    }

    public List<Item> GetInventoryItems()
    {
        List<Item> aux = new List<Item>();

        foreach(Item i in _EveryItemList)
        {
            if(i.inventoryUse != Item.InventoryUse.PAYMENT) aux.Add(i);
        }

        return aux;
    }

    public List<Item> GetAllItems() => _EveryItemList;
}
