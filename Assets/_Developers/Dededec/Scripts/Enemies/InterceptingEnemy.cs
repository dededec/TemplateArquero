using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptingEnemy : EnemyBase
{

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _player;
    [SerializeField] private float _timeToRecalculate;
    [SerializeField] private float _interceptSpeed = 25f;

    // Pausa
    private Vector3 _pausedVelocity;
    private Vector3 _pausedAngularVelocity;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(crInterceptBehaviour());
    }

    private void InterceptPlayer()
    {
        Vector3 force = _player.position + 5f * _player.forward - _rb.position; // Esto debería de ser la velocidad, y ser más visible de alguna forma.
        force = force.normalized * _interceptSpeed; 
        _rb.AddForce(force, ForceMode.Force);
    }

    private IEnumerator crInterceptBehaviour()
    {
        while(true)
        {
            if(_rb.velocity.magnitude < 7f)
            {
                transform.LookAt(_player.position);
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
            
            for(float i=0; i<= _timeToRecalculate; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }

            if(_rb.velocity.magnitude < 7f)
            {
                InterceptPlayer();
            }

            for(float i=0; i<= _timeToRecalculate; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }
        }
    }

    protected override void onGameStateChanged(GameState newGameState)
    {
        switch (GameStateManager.instance.CurrentGameState)
        {
            case GameState.Gameplay:
            ResumeRigidbody();
            break;
            case GameState.Paused:
            PauseRigidbody();
            break;
            default:
            break;
        }
    }

    private void PauseRigidbody() 
    {
        _pausedVelocity = _rb.velocity;
        _pausedAngularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
    }

    private void ResumeRigidbody() 
    {
        _rb.isKinematic = false;
        _rb.velocity = _pausedVelocity;
        _rb.angularVelocity = _pausedAngularVelocity;
    }
}