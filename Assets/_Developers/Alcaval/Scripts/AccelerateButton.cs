using UnityEngine;
 using UnityEngine.Events;
 using UnityEngine.EventSystems;
 using UnityEngine.UI;
 using System;

 public class AccelerateButton : Button
 {
     UnityEvent mOnDown = new UnityEvent();
     UnityEvent mOnUp = new UnityEvent();
     private GameObject _car;
     private CarController _cc;

    override protected void Start() {
            _car = GameObject.FindGameObjectWithTag("Car");
            if(_car != null) _cc = _car.GetComponent<CarController>();
            mOnDown.AddListener(engineUp);
            mOnUp.AddListener(frenada);
    }

     public override void OnPointerDown(PointerEventData eventData)
     {
         base.OnPointerDown(eventData);
         mOnDown.Invoke();
     }
 
     public override void OnPointerUp(PointerEventData eventData)
     {
         base.OnPointerUp(eventData);
         mOnUp.Invoke();
     }
 
     public UnityEvent onDown
     {
         get{ return mOnDown; }
     }
     
     public UnityEvent onUp
     {
         get{ return mOnUp; }
     }

    void engineUp()
    {
        _cc.engineUP();
    }

    void frenada()
    {
        _cc.frenada();
    }
 }