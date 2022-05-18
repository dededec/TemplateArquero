using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New World", menuName = "World")]
public class World : ScriptableObject
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

    public int index;
    public int stages;
    public int maxBosses;
    public int maxEasyStages;
    public int maxDifficultStages;

    // Referencia a recompensas de llegar al nivel tal y cual
    public List<LevelAchievement> rewards;
}
