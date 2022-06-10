using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _currentHero;
    [SerializeField] private GameObject _EquippedHero;
    public void ChooseHero(int id)
    {
        for(int i = 0; i < _currentHero.transform.childCount; i++)
        {
            if(i != id)
            {
                _currentHero.transform.GetChild(i).gameObject.SetActive(false);
                _EquippedHero.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                _currentHero.transform.GetChild(i).gameObject.SetActive(true);
                _EquippedHero.transform.GetChild(i).gameObject.SetActive(true);
                // TODO: El jugador tiene a este hero? habria que guardarlo en el save data
            }
        }
    }
}
