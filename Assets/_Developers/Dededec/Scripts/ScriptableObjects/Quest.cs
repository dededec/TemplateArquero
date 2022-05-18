using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

public class Quest : ScriptableObject
{

    // ! Algo que represente el objetivo a cumplir
    public UnityAction questObjective;

    public List<Reward> rewards;

    public Quest()
    {
        rewards = new List<Reward>();
        // OnQuestComplete += QuestCompleted();
    }

    public Quest(UnityAction a)
    {
        questObjective += a;
    }

}
