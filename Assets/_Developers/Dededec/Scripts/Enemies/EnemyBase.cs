using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected static LevelFlow _flow;
    [SerializeField] protected UnityEvent OnStart;

    public void AssignLevelFlow(LevelFlow flow)
    {
        _flow = flow;
    }

    private void Awake() 
    {
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() 
    {
        _flow.DeleteEnemy(this);
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }

    protected virtual void onGameStateChanged(GameState newGameState){}
}
