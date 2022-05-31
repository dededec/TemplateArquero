using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : EnemyBase
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Transform _player;
    [SerializeField] protected float _timeToRecalculate;

    #region Virtual Methods
    
    protected virtual void Start() 
    {
        StartCoroutine(crFollowPlayer());
    }

    #endregion


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject);
        }
    }

    #region Base Methods

    protected override void onGameStateChanged(GameState newGameState)
    {
        switch (GameStateManager.instance.CurrentGameState)
        {
            case GameState.Gameplay:
            _agent.isStopped = false;
            break;
            case GameState.Paused:
            _agent.isStopped = true;
            break;
            default:
            break;
        }
    }

    private IEnumerator crFollowPlayer()
    {
        while(true)
        {
            _agent.SetDestination(_player.position);
            for(float i=0; i<= _timeToRecalculate; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }
        }
    }

    #endregion
}
