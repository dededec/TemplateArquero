using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarShooting : MonoBehaviour
{

    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shotCadence;
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
            
            for(float i=0; i<=_shotCadence; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }
        }
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        Instantiate(_bullet, position, rotation);
    }

    private void shoot()
    {   
        GameObject bullet = Instantiate(_bullet, transform.position + transform.forward, transform.rotation);
        _abilities.Shoot();
    }
}
