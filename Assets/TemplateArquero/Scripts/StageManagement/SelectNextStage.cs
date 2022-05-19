using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectNextStage : MonoBehaviour
{
    public void AdvanceStage()
    {
        WorldManager.AdvanceStage();
    }
}
