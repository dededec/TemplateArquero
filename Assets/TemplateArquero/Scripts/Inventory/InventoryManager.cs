using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;


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
    [SerializeField] public Item[] _playerEquipment = new Item[6];
    [SerializeField] private Sprite blank;

    public Item[] Equipment
    {
        get
        {
            return _playerEquipment;
        }
    }

    private void Awake() {
        _itemDatabase.loadData();
        loadData();
        setBag();
        setEquipment();
        blank = EquipmentPanels.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        var tex = Resources.Load<Texture2D>("Blank");
        var sprite = Sprite.Create(tex, new Rect(0.0f,0.0f,tex.width,tex.height), new Vector2(0.5f,0.5f), 100.0f);
        blank = sprite;
        blank.name = "blank";
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
        GameObject.FindGameObjectWithTag("DailyManager").GetComponent<DailyQuestsManager>().ProgressQuest("EquipmentFuse");
        receptor.multiplier += 1;
    }


    public void LevelUp(Item receptor)
    {
        receptor.level += 1;
        GameObject.FindGameObjectWithTag("DailyManager").GetComponent<DailyQuestsManager>().ProgressQuest("EquipmentUpgrade");
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

    [SerializeField] private GameObject EquipmentPanels;

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
            var tex = Resources.Load<Texture2D>(i.name);
            var sprite = Sprite.Create(tex, new Rect(0.0f,0.0f,tex.width,tex.height), new Vector2(0.5f,0.5f), 100.0f);
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

            EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = sprite;
            EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite.name = i.name;

            if(_playerEquipment[slot] != null)
            {
                AddToInventory(_playerEquipment[slot].id);
            }

            _playerEquipment[slot] = i;
        }
        setBag();
        saveData();
        
    }

    public void RemoveEquipment(string id)
    {
        int i = 0;
        foreach(Item itm in _playerEquipment)
        {
            if(itm.id == id)
            {
                _playerEquipment[i] = null;

                int slot = 0;
                switch(itm.inventoryUse)
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
                EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = blank;
                AddToInventory(itm.id);
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
                    _playerEquipment[j] = i;
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
        for(int i = 0; i < _playerEquipment.Length; i++)
        {
            if(_playerEquipment[i] == null)
            {
                SaveDataController.Equipment += "None;";
            }else{
                SaveDataController.Equipment += _playerEquipment[i].id + "-" + _playerEquipment[i].level + "-" + _playerEquipment[i].multiplier +";";
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
            //btn.GetComponent<Button>().onClick.AddListener(delegate { AssignEquipment(i.id); });
            btn.GetComponent<Button>().onClick.AddListener(delegate { });
            btn.transform.SetParent(gridLayoutGroup.transform);

            var tex = Resources.Load<Texture2D>(i.name);
            var sprite = Sprite.Create(tex, new Rect(0.0f,0.0f,tex.width,tex.height), new Vector2(0.5f,0.5f), 100.0f);
            btn.GetComponentInChildren<Image>().sprite = sprite;
            btn.GetComponentInChildren<Image>().sprite.name = i.name;
            
            //btn.GetComponentInChildren<TextMeshProUGUI>().text = i.name;
            btn.GetComponent<RectTransform>().localScale = Vector3.one;
            btn.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(btn.transform.position.x, btn.transform.position.y, 0);

        }

    }

    private void setEquipment()
    {
        int slot = 0;
        foreach(Item i in _playerEquipment)
        {
            if(i != null)
            {
                var tex = Resources.Load<Texture2D>(i.name);
                tex.name = i.name;
                var sprite = Sprite.Create(tex, new Rect(0.0f,0.0f,tex.width,tex.height), new Vector2(0.5f,0.5f), 100.0f);
                sprite.name = i.name;
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
                EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = sprite;
                EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().name = i.name;
                EquipmentPanels.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite.name = i.name;
            }
        }
    }
}
