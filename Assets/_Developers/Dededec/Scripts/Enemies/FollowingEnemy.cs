using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] private float _timeToRecalculate;

    private void Start() 
    {
        StartCoroutine(crFollowPlayer());
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
}
