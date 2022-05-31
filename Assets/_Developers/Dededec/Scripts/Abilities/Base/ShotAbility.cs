using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAbility : Ability
{
    public bool OneExec;
    public CarShooting shooting;

    public void AddAbility()
    {
        if(OneExec)
        {
            Activate();
        }
        else
        {
            shooting = GameObject.FindGameObjectWithTag("Car").GetComponent<CarShooting>();
            // shooting.ShotAbilities.AddListener(Activate);
        }
    }

    public virtual void Activate(){}
}
