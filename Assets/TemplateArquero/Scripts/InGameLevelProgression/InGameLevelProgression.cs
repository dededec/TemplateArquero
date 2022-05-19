using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InGameLevelProgression : MonoBehaviour
{
    #region Serialized fields
        
    #endregion


    #region Private vars

    private GameObject _levelBar;
    private GameObject _levelText;
    private int _currentExperience = 0;
    private int _currentLevel = 1;
    private int _stackedLevels = 0;

    #endregion

    #region Life Cycle

    private void Awake() 
    {
        _levelText = GameObject.FindGameObjectWithTag("InGameLevelText");
        _levelText.GetComponent<TextMeshProUGUI>().text = _currentLevel + "";

        _levelBar = GameObject.FindGameObjectWithTag("InGameLevelBar");
        var bar = _levelBar.transform as RectTransform;
        bar.sizeDelta = new Vector2 (0, bar.sizeDelta.y);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddExperience(5);
        }
    }
        
    #endregion

    #region Public Methods
        
    public void AddExperience(int quantity)
    {
        var bar = _levelBar.transform as RectTransform;
        bar.sizeDelta = new Vector2 (bar.sizeDelta.x + quantity, bar.sizeDelta.y);
        _currentExperience += quantity;

        if(_currentExperience >= 100)
        {
            bar.sizeDelta = new Vector2 (100 - _currentExperience, bar.sizeDelta.y);
            _currentExperience = 100 - _currentExperience;
            _currentLevel++;
            _stackedLevels++;
            _levelText.GetComponent<TextMeshProUGUI>().text = _currentLevel + "";
        }
    }

    public void CheckStackedLevels()
    {
        if(_stackedLevels > 0)
        {
            _stackedLevels--;
            // You should be able to choose a new ability or modifier
        }
    }

    #endregion
}
