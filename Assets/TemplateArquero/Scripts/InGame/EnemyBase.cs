using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected static LevelFlow _flow;
    [SerializeField] protected int _softCoinDrop;

    [SerializeField] protected Animator _animator;

    [Header("Extra drops")]
    [SerializeField] protected Item[] _itemDrop;

    public int SoftCoinDrop
    {
        get
        {
            return _softCoinDrop;
        }
    }

    public Item[] ItemDrop
    {
        get
        {
            return _itemDrop;
        }
    }

    #region Life Cycle

    protected void Awake() 
    {
        // _mainCamera = Camera.main;
        gameObject.tag = "Enemy";
        _health = _maxHealth;
        _animator = GetComponentInChildren<Animator>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    // protected virtual void Update() 
    // {
    //     // Offset position above object bbox (in world space)
    //     float offsetPosZ = transform.position.z + 1.5f;
    //     float offsetPosY = transform.position.y + 1.5f;

    //     // Final position of marker above GameObject in world space
    //     Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, offsetPosZ);

    //     // Calculate *screen* position (note, not a canvas/recttransform position)
    //     Vector2 screenPoint = _mainCamera.WorldToScreenPoint(offsetPos);

    //     // Set
    //     _healthSlider.GetComponent<RectTransform>().position = screenPoint;
    // }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
        
        dropSoftCoin();
    }

    #endregion

    public void AssignLevelFlow(LevelFlow flow)
    {
        _flow = flow;
    }

    public virtual void TakeDamage(int amount)
    {
        _health -= amount;
        // _healthSlider.fillAmount = (float)_health/(float)_maxHealth;
        if(_health <= 0)
        {
            _animator.SetTrigger("IsDead");
            _flow.DeleteEnemy(this);
        }
        else
        {
            _animator.SetTrigger("IsDamaged");
            StartCoroutine(crDamageTaken());
        }
    }

    private IEnumerator crDamageTaken()
    {
        var mat = GetComponentInChildren<Renderer>().material;
        for(float i=0; i<0.5f; i += Time.deltaTime)
        {
            mat.color = mat.color == Color.white ? mat.color = Color.red : mat.color = Color.white;
            yield return null;
        }
        mat.color = Color.white;
    }

    private void dropSoftCoin()
    {
        Debug.Log("Se dropea soft coins");
        /*
            * Se instancian las monedas f??sicas
            * ya que la gesti??n de la soft coin se hace
            ? desde LevelFlow.cs
        */
    }

    protected virtual void onGameStateChanged(GameState newGameState){}
}
