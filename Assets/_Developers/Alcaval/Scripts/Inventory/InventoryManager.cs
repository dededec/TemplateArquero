using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    // LIST OF EVERY ITEM IN THE GAME, LOADED BY A CSV
    [SerializeField] public List<Item> _EveryItemList;

    //INVENTORY
    [SerializeField] public List<Item> _PlayerItems; 

    //EQUIPMENT OF THE PLAYER
    [SerializeField] private Item[] _PlayerEquipment = new Item[6];

    private void Awake() {
        loadData();
    }

    public void MergeItems(Item receptor, List<Item> items)
    {
        foreach(Item i in items)
        {
            foreach(Item pi in _PlayerItems)
            {
                if(i.id == pi.id)
                {
                    _PlayerItems.Remove(pi);
                    break;
                }
            }
        }

        // ? Habria que decidir como se aumenta el multiplicador
        receptor.multiplier += 1;
    }

    public void LevelUp(Item receptor)
    {
        receptor.level += 1;
    }

    public void AddToInventory(string id)
    {
        Item i = null;
        foreach(Item itm in _EveryItemList)
        {
            if(itm.id == id)
            {
                i = itm;
                i.level = 1;
                i.multiplier = 1f;
                break;
            }
        }

        _PlayerItems.Add(i);
        saveData();
    }

    public void AssignEquipment(string id)
    {
        Item i = null;
        foreach(Item itm in _PlayerItems)
        {
            if(itm.id == id)
            {
                i = itm;
                _PlayerItems.Remove(itm);
                break;
            }
        }
        if(i != null)
        {    
            int slot = 0;
            switch(i.inventoryUse)
            {
                case Item.InventoryUse.SLOT1:
                    slot = 0;
                    break;
                case Item.InventoryUse.SLOT2:
                    slot = 1;
                    break;
                case Item.InventoryUse.SLOT3:
                    slot = 2;
                    break;
                case Item.InventoryUse.SLOT4:
                    slot = 3;
                    break;
                case Item.InventoryUse.ACCESORIES:
                    slot = 4;
                    break;
            }

            if(_PlayerEquipment[slot] != null)
            {
                AddToInventory(_PlayerEquipment[slot].id);
            }

            _PlayerEquipment[slot] = i;
        }
        saveData();
    }

    public void RemoveEquipment(string id)
    {
        int i = 0;
        foreach(Item itm in _PlayerEquipment)
        {
            if(itm.id == id)
            {
                _PlayerEquipment[i] = null;
                break;
            }
            i++;
        }
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

        string[] inventoryList = SaveDataController.Inventory.Split(";");
        foreach(string s in inventoryList)
        {
            string[] idLevelMult = s.Split("-");
            foreach(Item i in _EveryItemList)
            {
                if(i.id == idLevelMult[0])
                {
                    i.level = Convert.ToInt32(idLevelMult[1]);
                    i.multiplier = float.Parse(idLevelMult[2]);
                    _PlayerItems.Add(i);
                    break;
                }
            }
        }

        string[] equipmentList = SaveDataController.Equipment.Split(";");
        for(int j = 0; j < equipmentList.Length - 1; j++)
        {
            print(j);
            string[] idLevelMult = equipmentList[j].Split("-");
            foreach(Item i in _EveryItemList)
            {
                if(i.id == idLevelMult[0])
                {
                    i.level = Convert.ToInt32(idLevelMult[1]);
                    i.multiplier = float.Parse(idLevelMult[2]);
                    _PlayerEquipment[j] = i;
                    break;
                }
            }
        }

        // TODO Habria que asignar en la escena en el canvas el equipamiento y el inventario
    }

    public void saveData()
    {
        SaveDataController.Inventory = "";
        for(int i = 0; i < _PlayerItems.Count; i++)
        {
            SaveDataController.Inventory += _PlayerItems[i].id + "-" + _PlayerItems[i].level + "-" + _PlayerItems[i].multiplier +";";
        }

        SaveDataController.Equipment = "";
        for(int i = 0; i < _PlayerEquipment.Length; i++)
        {
            if(_PlayerEquipment[i] == null)
            {
                SaveDataController.Equipment += "None;";
            }else{
                SaveDataController.Equipment += _PlayerEquipment[i].id + "-" + _PlayerEquipment[i].level + "-" + _PlayerEquipment[i].multiplier +";";
            }
        }
    }
}
