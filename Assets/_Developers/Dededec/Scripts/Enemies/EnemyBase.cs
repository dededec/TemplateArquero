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

    private Coroutine _poisonCoroutine = null;
    private Coroutine _burnCoroutine = null;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] protected Image _healthSlider;

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
        gameObject.tag = "Enemy";
        _health = _maxHealth;
        _mainCamera = Camera.main;
        _animator = GetComponentInChildren<Animator>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    protected void Update() 
    {
        // Offset position above object bbox (in world space)
        float offsetPosZ = transform.position.z + 1.5f;
        float offsetPosY = transform.position.y + 1.5f;

        // Final position of marker above GameObject in world space
        Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, offsetPosZ);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 screenPoint = _mainCamera.WorldToScreenPoint(offsetPos);

        // Set
        _healthSlider.GetComponent<RectTransform>().position = screenPoint;
    }

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
        _healthSlider.fillAmount = (float)_health/(float)_maxHealth;
        if(_health <= 0)
        {
            // Destruir cositas
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
            * Se instancian las monedas físicas
            * ya que la gestión de la soft coin se hace
            ? desde LevelFlow.cs
        */
    }

    protected virtual void onGameStateChanged(GameState newGameState){}

    #region Cosas de los venenos y las quemaduras

    public void ApplySpark() 
    {
        /*
        Ver enemigos a menos de x distancia (con OverlapSphere)
        y dañarlos en PlayerStats.attackDamage * 0.25f

        Limitar: Solo se puede un rayo por enemigo por "tick"
        Podríamos hacer una coroutine que, si se puede, lance el rayo
        espere el tiempo del tick (2/7 segundos) y ponga disponible el spark
        de nuevo.
        */
        Debug.Log("Spark");
    }

    public void ApplyFreeze()
    {
        /*
        Ni idea de cómo meterlo la verdad
        */
        Debug.Log("Freeze");
    }

    public void ApplyPoison() 
    {
        if(_poisonCoroutine == null)
        {
            _poisonCoroutine = StartCoroutine(crApplyPoison());
        }
    }
    private IEnumerator crApplyPoison()
    {
        int damage = (int)((float) PlayerStats.instance.attackDamage * 0.35f);
        while(_health > 0)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(damage);
        }
        _poisonCoroutine = null;
    }

    public void ApplyBurn()
    {
        if(_burnCoroutine == null)
        {
            _burnCoroutine = StartCoroutine(crApplyBurn());
        }
    }
    private IEnumerator crApplyBurn()
    {
        float duracion = 2f;
        int damage = (int)((float) PlayerStats.instance.attackDamage * 0.18f);
        for(float i = 0f; i < duracion && _health > 0; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.25f);
            TakeDamage(damage);
        }

        _burnCoroutine = null;
    }

    #endregion
}
