using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentInfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _equipmentMessage;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private InventoryManager _inventory;

    public void SetEquipmentMessage(bool equipped)
    {
        Item currentItem = null;
        if(gameObject.GetComponent<Image>().sprite.name != "blank")
        {
            if(equipped)
            {
                foreach(Item i in _inventory._playerEquipment)
                {
                    print("Nombre del gameobject: " + gameObject.GetComponent<Image>().sprite.name);
                    if(i != null && i.name == gameObject.GetComponent<Image>().sprite.name)
                    {
                        currentItem = i;
                        break;
                    }
                }
                equipButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                equipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { _inventory.RemoveEquipment(currentItem.id); });
            } 
            else
            {
                foreach(Item i in _inventory._PlayerItems)
                {
                    print("Nombre del gameobject: " + gameObject.GetComponent<Image>().sprite.name);
                    if(i != null && i.name == gameObject.GetComponent<Image>().sprite.name)
                    {
                        currentItem = i;
                        break;
                    }
                }
                equipButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                equipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { _inventory.AssignEquipment(currentItem.id); });
            }
        }
        else
        {

        }

        print(currentItem);

        if(currentItem != null)
        {
            for(int i = 0; i < _equipmentMessage.transform.childCount; i++)
            {
                if(_equipmentMessage.transform.GetChild(i).name == "ImageItem")
                {
                    var tex = Resources.Load<Texture2D>(currentItem.name);
                    var sprite = Sprite.Create(tex, new Rect(0.0f,0.0f,tex.width,tex.height), new Vector2(0.5f,0.5f), 100.0f);
                    _equipmentMessage.transform.GetChild(i).GetComponent<Image>().sprite = sprite;
                }
            }

            TextMeshProUGUI[] texts = _equipmentMessage.GetComponentsInChildren<TextMeshProUGUI>();
            foreach(TextMeshProUGUI text in texts)
            {
                if(text.gameObject.name == "Name") text.text = currentItem.name;
                if(text.gameObject.name == "Equip")
                {
                    if(equipped) text.text = "Unequip";
                    else text.text = "Equip";
                }

                if(text.gameObject.name == "Stats")
                {
                    text.text = currentItem.stats;
                }
            }
        }
    }
}
