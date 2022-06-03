using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
#region Fields

    [Tooltip("Bullet speed when created.")]
	[SerializeField] private float _speed;
	[SerializeField] private float _lifeTime;
    
    private Rigidbody _rb;
    private Vector3 _pausedVelocity;
    private Vector3 _pausedAngularVelocity;
	  
	#endregion
	  
	#region LifeCycle

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }

    private void onGameStateChanged(GameState newGameState)
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
	  
    // Start, OnAwake, Update, etc
    private void OnEnable()   
    {
        GetComponent<Rigidbody>().velocity = transform.forward * _speed;
        StartCoroutine(crDestroy());
    }

    private void OnDisable() 
    {
        StopAllCoroutines();
    }

    #endregion

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

    private IEnumerator crDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Car")
        {
            other.gameObject.GetComponent<CarHealthManager>().TakeDamage(20f);
        }    
        
        gameObject.SetActive(false);
    }
}
