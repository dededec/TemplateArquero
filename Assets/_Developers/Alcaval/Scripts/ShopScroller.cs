using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScroller : MonoBehaviour
{
    [SerializeField] private Scrollbar sBar;
    [SerializeField] private float totalRows;
    public bool focus = false;
    public float debugValue = 0;

    // Update is called once per frame
    void Update()
    {
        debugValue = sBar.value;
    }

    public void FocusOn(float row)
    {
        sBar.value = 1 - row/totalRows;
        print(sBar.value);
    }
}
