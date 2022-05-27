using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRendererLeft;
    [SerializeField] private TrailRenderer _trailRendererRight;
    [SerializeField] private float _lifeTime = 3f;

    private void Awake() 
    {
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
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
