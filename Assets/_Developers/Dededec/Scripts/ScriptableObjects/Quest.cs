using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

[CreateAssetMenu(fileName = "New Quest/Achievement", menuName = "Quest | Achievement")]
public class Quest : ScriptableObject
{
    [Tooltip("ID de la quest/logro. Debe empezar con \"a\" (Achievement/Logro) o \"q\" (Quest/Misión) ")]
    public string id;

    // ! Algo que represente el objetivo a cumplir
    
    public UnityAction OnQuestCompleted;

    public List<Reward> rewards;

    public int progress;

    public int AddProgress(int a)
    {
        progress += a;
        return progress;
    }

}
