using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanController : MonoBehaviour
{
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.localScale = Vector2.zero;
    }

    public void Toggle()
    {
        if(state)
        {
            transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
            state = false;
        }
        else
        {
            transform.LeanScale(Vector2.one, 0.3f);
            state = true;
        }
    }
}
