using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LoopJam;

public class CarShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [Header("Autoaim")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _angleThreshold;
    [SerializeField] private float _detectionRadius;

    private void Start() 
    {
       StartCoroutine(crShooting());
    }

    private IEnumerator crShooting()
    {
        while(true)
        {
            shoot();
            
            for(float i=0; i<=1/PlayerStats.instance.attackSpeed; i+=Time.deltaTime)
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
        GameObject bullet = Instantiate(_bullet, transform.position + transform.forward, autoAimRotation());
        AbilityManager.instance.ShootAbilities();
    }

    private Quaternion autoAimRotation()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _enemyLayer);
        if(hitColliders.Length == 0)
        {
            return transform.rotation;
        }

        float minAngle = Mathf.Infinity;
        Collider closest = null;
        foreach(var collider in hitColliders)
        {
            float angle = Vector3.Angle(transform.forward, collider.transform.position - transform.position);
            if(angle < minAngle)
            {
                minAngle = angle;
                closest = collider;
            }
        }

        if(minAngle < _angleThreshold)
        {
            return Quaternion.LookRotation(closest.transform.position - transform.position, Vector3.up);
        }
        else
        {
            return transform.rotation;
        }

    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(_bullet, position, rotation);
    }

    public void Shoot(Vector3 position)
    {
        Shoot(position, autoAimRotation());
    }

    public void IncreaseDamage(int amount)
    {
        PlayerStats.instance.attackDamage += amount;
    }

    public void IncreaseAttackSpeed(int amount)
    {
        PlayerStats.instance.attackSpeed += amount;
    }
}
