using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class LoadPlay : MonoBehaviour
{
    [SerializeField] private GameFlowController gfc;
    [SerializeField] private Button _playButton;

    private void Start() 
    {
        _playButton.onClick.AddListener(delegate { WorldManager.Play();});
    }

    public void loadPlay()
    {
        gfc.LoadScene("Game");
    }
}
