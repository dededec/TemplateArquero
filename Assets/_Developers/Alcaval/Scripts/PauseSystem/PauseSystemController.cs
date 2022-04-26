using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystemController : MonoBehaviour
{
    [SerializeField] private GameObject _pausedMenu;
    public void PauseGame()
    {
        _pausedMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _pausedMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToHome()
    {

    }

    public void ToggleVolume()
    {

    }
}
