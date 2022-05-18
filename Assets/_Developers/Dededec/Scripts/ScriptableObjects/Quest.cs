using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

public class Quest : ScriptableObject
{

    public string id;

    // ! Algo que represente el objetivo a cumplir
    public UnityAction OnQuestCompleted;

    public List<Reward> rewards;

    public float progress;

    public Quest()
    {
        rewards = new List<Reward>();
        progress = 0f;
    }

    public float AddProgress(float a)
    {
        progress += a;
        return progress;
    }

}
