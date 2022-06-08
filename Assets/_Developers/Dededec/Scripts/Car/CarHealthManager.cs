using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHealthManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CMScreenshake _cmScreenshake;
    [SerializeField] private CarController _carController;
    [SerializeField] private LevelFlow _levelFlow;

    [Header("UI/VFX")]
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private Material _carMaterial;
    [SerializeField] private Color _originalCar;
    [SerializeField] private Color _damageColor;
    [SerializeField] private AudioSource _damageAudio;
    [SerializeField] private Slider _healthSlider;

    //HealthThings
    // [Header("Stats")]
    // public int maxHealth = 100;
    // public int damageReductionStill;
    // public int damageReductionMoving;
    // public int dropHealing;
    // public int levelUpHealing;
    // private int _currentHealth = 100;
    private bool _isHealing = false;


    public int CurrentHealth
    {
        get
        {
            return PlayerStats.instance.currentHealth;
        }

        set
        {
            PlayerStats.instance.currentHealth = value;
            _healthSlider.value = PlayerStats.instance.currentHealth;
        }
    }
    
    private void Awake() 
    {
        _carController = GetComponent<CarController>();
        _carMaterial.color = _originalCar;
    }

    private void Update()
    {
        if(CurrentHealth > 0 && GameStateManager.instance.CurrentGameState == GameState.Gameplay && !_isHealing)
        {
            StartCoroutine(HealCoroutine());
        }
    }

    public void TakeDamage(int value)
    {
        _cmScreenshake.ShakeCamera(8, 0.5f);
        
        if(_carController.IsMoving())
        {
            value -= PlayerStats.instance.damageReductionMoving;
        }
        else
        {
            value -= PlayerStats.instance.damageReductionStill;
        }

        CurrentHealth -= value;

        if(CurrentHealth <= 0)
        {
            _levelFlow.PlayerDeath();
            Destroy(AbilityManager.instance);
            gameObject.SetActive(false);
            _gameplayUI.SetActive(false);
            _gameOverMenu.SetActive(true);
        }
        else
        {
            StartCoroutine(takeDamageCoroutine());
        }   
    }

    public void LevelUp()
    {
        Heal(PlayerStats.instance.levelUpHealing);
    }

    public void OnDropPickup()
    {
        Heal(PlayerStats.instance.dropHealing);
    }

    private void Heal(int amount)
    {
        if(amount < 0 ) return;
        CurrentHealth += amount;
    }
    
    private IEnumerator HealCoroutine()
    {   
        if(!_carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            _isHealing = true;
            for(float i=0; i<1f; i+=Time.deltaTime)
            {
                do
                {
                    yield return null;
                }while(GameStateManager.instance.CurrentGameState == GameState.Paused);
            }
            // print("Healing.");
            CurrentHealth += 1;
            _isHealing = false;
        }
    }

    private IEnumerator takeDamageCoroutine()
    {
        //Se cambia el color
        _carMaterial.color = _damageColor;
        _damageAudio.Play();
        yield return new WaitForSeconds(0.5f);
        //Se vuelve a poner
        _carMaterial.color = _originalCar;
    }
}
