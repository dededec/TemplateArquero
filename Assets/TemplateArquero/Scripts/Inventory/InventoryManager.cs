using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


/*
* Once you have a GameObject in the scene with this script you can access all the functionalities of the inventory and the equipment  
* It is needed to provide a csv file called ItemDatabase following the example at "/Resources"
*/
public class InventoryManager : MonoBehaviour
{
    
    [SerializeField] private ItemDatabaseManager _itemDatabase;
    [SerializeField] private List<Item> _EveryItemList;

    //INVENTORY
    [SerializeField] public List<Item> _PlayerItems; 

    //EQUIPMENT OF THE PLAYER
    [SerializeField] private Item[] _PlayerEquipment = new Item[6];

    private void Awake() {
        loadData();
        setBag();
    }

    // * Call when button pressed to merge a piece of equipment with other two of the same type of item
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
        setBag();
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
        setBag();
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
        _EveryItemList = _itemDatabase.GetInventoryItems();

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

    [SerializeField] GameObject equipmentButton;
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    private void setBag()
    {
        foreach(Transform t in gridLayoutGroup.transform)
        {
            Destroy(t.gameObject);
        }

        foreach(Item i in _PlayerItems)
        {
            GameObject btn;
            btn = Instantiate(equipmentButton);
            btn.GetComponent<Button>().onClick.AddListener(delegate { AssignEquipment(i.id); });
            btn.transform.SetParent(gridLayoutGroup.transform);
            btn.GetComponent<RectTransform>().localScale = Vector3.one;

        }

    }
}
