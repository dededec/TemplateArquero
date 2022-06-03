using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _equipmentMessage;
    [SerializeField] private InventoryManager _inventory;

    public void SetEquipmentMessage()
    {
        Item currentItem = null;
        foreach(Item i in _inventory._playerEquipment)
        {
            print(gameObject.GetComponent<Image>().sprite.name);
            if(i.name == gameObject.GetComponent<Image>().sprite.name)
            {
                currentItem = i;
                break;
            }
        }

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
            print("se deberia printar las cosas");
        }
    }
}
