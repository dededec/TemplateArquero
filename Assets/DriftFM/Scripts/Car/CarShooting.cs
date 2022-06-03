using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LoopJam;

public class CarShooting : MonoBehaviour
{

    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private AbilityManager _abilities;

    private void Start() 
    {
       StartCoroutine(crShooting());
    }

    private IEnumerator crShooting()
    {
        while(true)
        {
            shoot();
            
            for(float i=0; i<=1/_attackSpeed; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }
        }
    }

    private void shoot()
    {   
        GameObject bullet = Instantiate(_bullet, transform.position + transform.forward, transform.rotation);
        _abilities.Shoot();
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        Instantiate(_bullet, position, rotation);
    }

    public void IncreaseDamage(int amount)
    {
        _bullet.GetComponent<BulletBehaviour>().IncreaseDamage(amount);
    }

    public void IncreaseAttackSpeed(int amount)
    {
        _attackSpeed += amount;
    }
}
