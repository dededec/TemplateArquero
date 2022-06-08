using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected static LevelFlow _flow;
    [SerializeField] protected int _softCoinDrop;
    [SerializeField] protected int _experienceDrop;

    [Header("Extra drops")]
    [SerializeField] protected bool _extraDrop;
    // ? ¿Cómo represento los drops?

    public int SoftCoinDrop
    {
        get
        {
            return _softCoinDrop;
        }
    }

    public int Experience
    {
        get
        {
            return _experienceDrop;
        }
    }

    public void AssignLevelFlow(LevelFlow flow)
    {
        _flow = flow;
    }

    public virtual void TakeDamage(int amount)
    {
        _health -= amount;
        if(_health <= 0)
        {
            // Destruir cositas
            _flow.DeleteEnemy(this);
        }
    }

    private void Awake() 
    {
        gameObject.tag = "Enemy";
        _health = _maxHealth;
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;

        if(_extraDrop)
        {
            Debug.Log("Se dropean cosas extra");
        }
        
        dropSoftCoin();
    }

    private void dropSoftCoin()
    {
        Debug.Log("Se dropea soft coins");
        /*
            * Se instancian las monedas físicas
            * ya que la gestión de la soft coin se hace
            ? desde LevelFlow.cs
        */
    }

    protected virtual void onGameStateChanged(GameState newGameState){}
}
