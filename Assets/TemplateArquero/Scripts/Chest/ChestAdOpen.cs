using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestAdOpen : TimedObject
{
    [SerializeField] private float _timeToAd;
    [SerializeField] private Button _button;
    protected override void Initialize()
    {
        System.TimeSpan timeSpan = _timeManager.TimeSinceLastConnection();
        if (timeSpan.TotalDays >= _timeToAd)
        {
            OnIntervalCompleted();
        }
    }

    protected override void OnIntervalCompleted()
    {
        // Activamos el botón de ver anuncio para abrir cofre.
        throw new System.NotImplementedException();
    }

    public void OnButtonClick()
    {
        
    }
}
