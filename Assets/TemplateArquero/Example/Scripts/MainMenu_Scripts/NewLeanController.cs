using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLeanController : MonoBehaviour
{
    private bool state = false;
    [SerializeField] private GameObject buttonPanel;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.localScale = Vector2.zero;
    }

    public void Toggle(int id)
    {
        if(state)
        {
            transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
            state = false;
        }
        else if(id == -1 || buttonPanel.transform.GetChild(id).GetChild(0).GetComponent<Image>().sprite.name != "blank")
        {
            transform.LeanScale(Vector2.one, 0.3f);
            state = true;
        }
    }
}
