using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameLevelProgression : MonoBehaviour
{
    #region Serialized fields
        
    #endregion


    #region Private vars

    private GameObject _levelBar;
    private TextMeshProUGUI _level;
    private int _currentExperience = 0;
    private int _currentLevel = 1;

    #endregion

    #region Life Cycle

    private void Awake() 
    {
        _level.text = _currentLevel + "";
    }
        
    #endregion

    public void AddExperience()
    {

    }
}
