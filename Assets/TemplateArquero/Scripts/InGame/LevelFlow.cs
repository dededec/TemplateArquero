using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlow : MonoBehaviour
{   
    [SerializeField] private DailyQuestsManager _questManager;
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private ExitDoor _exitDoor;
    [SerializeField] private Transform _enemyHolder;
    [SerializeField] private List<EnemyBase> _enemies;

    private int _accSoftCoin = 0;
    private List<Reward> _accItem = new List<Reward>();
    [SerializeField] private InGameCoinCountController _coinCounter;
    [SerializeField] private InGameLevelProgression _levelprogression;

    public bool isPlayerAlive = true;

    private void Awake()
    {
        // Cogemos los enemigos (los hijos de enemies)
        for(int i=0; i < _enemyHolder.childCount; ++i)
        {
            if(_enemyHolder.GetChild(i).gameObject.activeSelf)
            {
                _enemies.Add(_enemyHolder.GetChild(i).GetComponent<EnemyBase>());
            }
        }
        
        if(GameObject.FindGameObjectWithTag("DailyManager") != null) _questManager = GameObject.FindGameObjectWithTag("DailyManager").GetComponent<DailyQuestsManager>();
        if(_enemies.Count != 0) _enemies[0].AssignLevelFlow(this);
    }

    public void DeleteEnemy(EnemyBase enemy)
    {
        _enemies.Remove(enemy);
        _accSoftCoin += enemy.SoftCoinDrop;
        foreach (var item in enemy.ItemDrop)
        {
            _accItem.Add(new Reward(item.id, 1));
        }
                
        Destroy(enemy.gameObject);
        
        if(_enemies.Count <= 0)
        {
            _coinCounter.AddCoinAmount(_accSoftCoin);
            _levelprogression.AddExperience(_accSoftCoin);
            _accSoftCoin = 0;
            
            // Obtener items
            _rewardManager.GiveReward(_accItem);
            _accItem.Clear();
            
            _exitDoor.enabled = true;
        }
    }

    public void PlayerDeath()
    {
        isPlayerAlive = false;
    }
}
