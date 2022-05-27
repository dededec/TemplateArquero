using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbility : Ability
{
    public bool OneExec;

    public void AddAbility()
    {
        if(OneExec)
        {
            Activate();
        }
        else
        {
            
        }
    }

    public virtual void Activate(){}
}
