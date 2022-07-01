using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : EnemyBase
{
    [Header("Following Settings")]
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Transform _player;
    [SerializeField] protected float _timeToRecalculate;

    #region Virtual Methods
    
    protected virtual void Start() 
    {
        _player = GameObject.FindGameObjectWithTag("Car").transform;
        StartCoroutine(crFollowPlayer());
    }

    #endregion

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
        _animator.SetBool("IsMoving", true);
        yield return new WaitForSeconds(3f);
        while(_flow.isPlayerAlive)
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

        _agent.isStopped = true;
        _animator.SetBool("IsMoving", false);
    }

    #endregion
}
