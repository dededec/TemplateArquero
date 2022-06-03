using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHealthManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CMScreenshake _cmScreenshake;
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private CarController _carController;

    [Header("UI/VFX")]
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private Material _carMaterial;
    [SerializeField] private Color _originalCar;
    [SerializeField] private Color _damageColor;
    [SerializeField] private AudioSource _damageAudio;
    [SerializeField] private Slider _healthSlider;

    //HealthThings
    [Header("Stats")]
    public int maxHealth = 100;
    public float damageReductionStill;
    public float damageReductionMoving;
    public float dropHealing;
    public float levelUpHealing;
    private float _currentHealth = 100f;
    private bool _isHealing = false;


    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = value;
            _healthSlider.value = _currentHealth;
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

    public void TakeDamage(float value)
    {
        _cmScreenshake.ShakeCamera(8, 0.5f);
        
        if(_carController.IsMoving())
        {
            value -= damageReductionMoving;
        }
        else
        {
            value -= damageReductionStill;
        }

        CurrentHealth -= value;

        if(CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            _gameOverMenu.SetActive(true);
        }

        StartCoroutine(takeDamageCoroutine());
    }

    public void LevelUp()
    {
        Heal(levelUpHealing);
    }

    public void OnDropPickup()
    {
        Heal(dropHealing);
    }

    private void Heal(float amount)
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
            print("Healing.");
            CurrentHealth += 1f;
            _isHealing = false;
        }
    }

    private IEnumerator takeDamageCoroutine()
    {
        //Se cambia el color
        _carMaterial.color = _damageColor;
        _damageAudio.Play();
        _mainMenuController.gameObject.transform.GetChild(0).gameObject.SetActive(false); // Radio
        yield return new WaitForSeconds(0.5f);
        _mainMenuController.gameObject.transform.GetChild(0).gameObject.SetActive(true); // Radio
        //Se vuelve a poner
        _carMaterial.color = _originalCar;
    }
}
