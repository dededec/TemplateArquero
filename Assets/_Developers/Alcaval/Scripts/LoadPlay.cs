using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlay : MonoBehaviour
{
    [SerializeField] private GameFlowController gfc;

    public void loadPlay()
    {
        gfc.LoadScene("Game");
    }
}
