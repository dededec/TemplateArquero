using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private CarController _carController;
    [SerializeField] private TrailRenderer _trailRendererLeft;
    [SerializeField] private TrailRenderer _trailRendererRight;
    [SerializeField] private float _lifeTime = 3f;

    private void Awake() 
    {
        _carController = GetComponent<CarController>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }

    private void Update()
    {
        if(_carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            _trailRendererLeft.emitting = true;
            _trailRendererRight.emitting = true;
        }
        else
        {
            _trailRendererLeft.emitting = false;
            _trailRendererRight.emitting = false;
        }
    }

    private void onGameStateChanged(GameState newGameState)
    {
        switch(GameStateManager.instance.CurrentGameState)
        {
            case GameState.Gameplay:
            _trailRendererLeft.time = _lifeTime;
            _trailRendererRight.time = _lifeTime;
            break;
            case GameState.Paused:
            _trailRendererLeft.time = Mathf.Infinity;
            _trailRendererRight.time = Mathf.Infinity;
            break;
            default:
            break;
        }
    }
}
