using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AngelBehaviour : MonoBehaviour
{
    [SerializeField] private AbilityManager _playerAbilities;

    // Start is called before the first frame update
    void Start()
    {
        _playerAbilities = GameObject.FindGameObjectWithTag("Car").GetComponent<AbilityManager>();

        var indexes = _playerAbilities.FindAbilitiesIndex(false);
        int[] abilities = new int[3];

        // Elegimos tres indices al azar.
        if(indexes.Count >= 3)
        {
            for(int i=0; i<3; ++i)
            {
                int random = -1;
                do
                {
                    random = UnityEngine.Random.Range(0, indexes.Count);
                }while(find(abilities, random));

                abilities[i] = random;
            }
        }
        else if(indexes.Count > 0)
        {
            for(int i=0; i<indexes.Count; ++i)
            {
                abilities[i] = indexes[i];
            }
        }
    }

    public bool find(int[] array, int target) 
    {
        return Array.Exists(array, x => x == target);
    }
}
