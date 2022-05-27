using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbility : Ability
{
    public bool OneExec;
    public CarController controller;

    public void AddAbility()
    {
        if(OneExec)
        {
            Activate();
        }
        else
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
            controller.MoveAbilities.AddListener(Activate);
        }
    }

    public virtual void Activate(){}
}
