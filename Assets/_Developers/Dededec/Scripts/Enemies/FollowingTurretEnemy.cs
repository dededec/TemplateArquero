using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingTurretEnemy : FollowingEnemy
{
    [SerializeField] private float _timeToShoot;
    [SerializeField] private GameObject _bullet;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(crShoot());
    }

    private IEnumerator crShoot()
    {
        while(_flow.isPlayerAlive)
        {
            Instantiate(_bullet, transform.position + transform.forward, Quaternion.LookRotation(_player.position - transform.position));
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
