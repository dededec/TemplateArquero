using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private Image icon;

    private void Start() {
        icon.sprite = ability.icon;
    }
    
}
