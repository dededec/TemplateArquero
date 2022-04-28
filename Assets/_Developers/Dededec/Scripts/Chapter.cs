using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Chapter")]
public class Chapter : ScriptableObject
{    
    [System.Serializable]
    public struct LevelAchievement
    {
        public int level;
        public UnityEvent reward;
    }
    
    public new string name;
    public string description;
    public Sprite icon;

    public int stages;
    public int highestStageReached;

    // Referencia a escena de Unity?
    public SceneAsset gameScene;

    // Referencia a recompensas de llegar al nivel tal y cual
    public List<LevelAchievement> rewards;
}
