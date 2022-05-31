using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "GameAbility")]
public class GameAbility : Ability
{
    public enum AbilityType
    {
        SHOT,
        MOVEMENT,
        BULLET,
    }

    public AbilityType type;
    public bool OneExec;

    public void AddAbility()
    {
        switch(type)
        {
            case AbilityType.SHOT:
            CarShooting shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<CarShooting>();
            if(OneExec)
            {
                Activate();
            }
            else
            {
                // shooting.ShotAbilities.AddListener(Activate);
            }
            break;
            
            case AbilityType.MOVEMENT:
            CarController controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
            if(OneExec)
            {
                Activate();
            }
            else
            {
                controller.MoveAbilities.AddListener(Activate);
            }
            break;

            case AbilityType.BULLET:
            break;
        }
    }

    public virtual void Activate(){}
}
