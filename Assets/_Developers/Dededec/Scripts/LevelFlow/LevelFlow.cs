using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlow : MonoBehaviour
{   
    [SerializeField] private ExitDoor _exitDoor;
    [SerializeField] private Transform _enemyHolder;
    [SerializeField] private List<EnemyBase> _enemies;
    [SerializeField] private InGameCoinCountController _coinCounter;
    [SerializeField] private InGameLevelProgression _levelprogression;

    public bool isPlayerAlive = true;

    private void Start()
    {
        // Cogemos los enemigos (los hijos de enemies)
        for(int i=0; i < _enemyHolder.childCount; ++i)
        {
            if(_enemyHolder.GetChild(i).gameObject.activeSelf)
            {
                _enemies.Add(_enemyHolder.GetChild(i).GetComponent<EnemyBase>());
            }
        }

        _enemies[0].AssignLevelFlow(this);
    }

    public void DeleteEnemy(EnemyBase enemy)
    {
        _enemies.Remove(enemy);
        // Subir soft coins
        _coinCounter.AddCoinAmount(enemy.SoftCoinDrop);
        _levelprogression.AddExperience(enemy.Experience);
        Destroy(enemy.gameObject);
        
        if(_enemies.Count <= 0)
        {
            // Se han eliminado a todos los enemigos.
            /*
            ¿Cómo se sale? - Si es por puerta, estará en el mundo visualmente
            y se activará un script o algo.
            */
            _exitDoor.enabled = true;
        }
    }

    public void PlayerDeath()
    {
        isPlayerAlive = false;
    }
}
