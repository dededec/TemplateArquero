using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystemController : MonoBehaviour
{
    [SerializeField] private GameObject _pausedMenu;
    [SerializeField] private AudioSource _audiosource;
    public void PauseGame()
    {
        _pausedMenu.SetActive(true);
        GameState newGameState = GameState.Paused;
        GameStateManager.instance.SetState(newGameState);
        // Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _pausedMenu.SetActive(false);
        GameState newGameState = GameState.Gameplay;
        GameStateManager.instance.SetState(newGameState);
        // Time.timeScale = 1f;
    }

    public void ReturnToHome()
    {
        //Load main scene
    }

    public void ToggleVolume()
    {
        if(_audiosource.volume == 0f) _audiosource.volume = 1f;
        else _audiosource.volume = 0f;
    }

    #region Example of new way of pausing game in each of the scripts of the things we need to pause or resume

    private void Awake() {
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }

    private void onGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
        print("uwu");
    }
        
    #endregion
}
