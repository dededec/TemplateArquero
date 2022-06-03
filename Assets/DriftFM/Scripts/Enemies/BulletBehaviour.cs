/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopJam
{
    /// <summary>  
	/// AI for bullets, which travels along their forward on creation
	/// </summary>
    public class BulletBehaviour : MonoBehaviour
    {
        #region Fields

        [Tooltip("Bullet speed when created.")]
	    [SerializeField] private float _speed;
	    [SerializeField] private float _lifeTime;
        [SerializeField] private float _damageDealt;
    
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

        public void IncreaseDamage(int amount)
        {
            _damageDealt += amount;
        }
    }
}
