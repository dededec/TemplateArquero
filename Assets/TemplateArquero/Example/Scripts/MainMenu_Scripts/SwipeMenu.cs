using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _scrollbar;
    private float[] pos;
    private float scroll_pos = 0;
    private bool touching;
    private float distance;
    public int currentSelectedWorld; 

    // Update is called once per frame
    void Update()
    {
        if(touching)
        {
            scroll_pos = _scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for(int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    _scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(_scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    currentSelectedWorld = i;
                }
            }
        }

        for(int i = 0; i < pos.Length; i++)
        {
            if(scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for(int j = 0; j < pos.Length; j++)
                {
                    if(j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

    }

     void OnEnable()
    {
        pos = new float[transform.childCount];
        distance = 1f/(pos.Length - 1f);
        for(int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += get_touch_details;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += get_touch_details;
    }

    void get_touch_details(Finger fin)
    {
        if(fin.currentTouch.began)
        {
            touching = true;
        }
        else if(fin.currentTouch.ended)
        {
            touching = false;
        }
        
    }
}
