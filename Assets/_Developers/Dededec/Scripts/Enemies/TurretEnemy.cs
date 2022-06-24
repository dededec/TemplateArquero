using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : FollowingEnemy
{
    [Header("Turret Behaviour")]
    [SerializeField] private float _timeToShoot;
    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private bool _isFollowing;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if(_flow == null)
        {
            Debug.Log("Flow es null.");
        }
        if(!_isFollowing)
        {
            StopAllCoroutines();
            _agent.isStopped = true;
            _animator.SetBool("IsMoving", false);
        }
        StartCoroutine(crShoot());
    }

    protected override void Update() {
        base.Update();
        
        transform.GetChild(0).transform.rotation = Quaternion.LookRotation(_player.transform.position, Vector3.up);
    }

    private IEnumerator crShoot()
    {
        yield return new WaitForSeconds(3f);
        while(_flow.isPlayerAlive)
        {
            if(Vector3.Distance(transform.position, _player.position) < _maxDistance)
            {
                _animator.SetTrigger("Attack");
                Instantiate(_bullet, transform.position + transform.forward, Quaternion.LookRotation(_player.position - transform.position));
                // Instantiate(_bullet, transform.position + transform.forward, Quaternion.LookRotation(_player.position * Random.Range(-1, 1) - transform.position));
                // Instantiate(_bullet, transform.position + transform.forward, Quaternion.LookRotation(_player.position * Random.Range(-1, 1) - transform.position));
                // Instantiate(_bullet, transform.position + transform.forward, Quaternion.LookRotation(_player.position * Random.Range(-1, 1) - transform.position));
            }

            for(float i=0; i<= _timeToShoot; i+=Time.deltaTime)
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
            if(_isFollowing)
            {
                _agent.isStopped = false;
            }
            break;
            case GameState.Paused:
            _agent.isStopped = true;
            break;
            default:
            break;
        }
    }
}
