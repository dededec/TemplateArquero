using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Variables related to the whole inventory
    [SerializeField] private List<Item> _EveryItemList;
    [SerializeField] public List<Item> _PlayerItems; 

    //Variables related to the equipment of the player
    [SerializeField] private Item[] _PlayerEquipment = new Item[6];

    private void Awake() {
        loadData();
    }

    public void AssignEquipment(Item i)
    {
        switch(i.inventoryUse)
        {
            case Item.InventoryUse.SLOT1:
                _PlayerEquipment[0] = i;
                break;
            case Item.InventoryUse.SLOT2:
                _PlayerEquipment[1] = i;
                break;
            case Item.InventoryUse.SLOT3:
                _PlayerEquipment[2] = i;
                break;
            case Item.InventoryUse.SLOT4:
                _PlayerEquipment[3] = i;
                break;
            case Item.InventoryUse.SLOT5:
                _PlayerEquipment[4] = i;
                break;
            case Item.InventoryUse.SLOT6:
                _PlayerEquipment[5] = i;
                break;
        }
    }

    public void loadData()
    {
        string[] inventoryList = SaveDataController.Inventory.Split(";");
        foreach(string s in inventoryList)
        {
            foreach(Item i in _EveryItemList)
            {
                if(i.id == s)
                {
                    _PlayerItems.Add(i);
                    break;
                }
            }
        }

        string[] equipmentList = SaveDataController.Equipment.Split(";");
        for(int j = 0; j < equipmentList.Length - 1; j++)
        {
            foreach(Item i in _EveryItemList)
            {
                if(i.id == equipmentList[j])
                {
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
            SaveDataController.Inventory += _PlayerItems[i].id + ";";
        }

        SaveDataController.Equipment = "";
        for(int i = 0; i < _PlayerEquipment.Length - 1; i++)
        {
            SaveDataController.Equipment += _PlayerEquipment[i].id + ";";
        }
    }
}
