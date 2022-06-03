using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CarController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private Joystick _joystick;

    private Rigidbody _carRB;
    
    [SerializeField] private float accelerationFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] float driftFactor = 0.95f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityUp = 0;
    private bool backwards = false;

    // Pausa
    private Vector3 _pausedVelocity;
    private Vector3 _pausedAngularVelocity;

    public UnityEvent MoveAbilities;


    // [SerializeField] private Slider health;
    // [SerializeField] private CMScreenshake cmscreenshake;
    // [SerializeField] private MainMenuController _mainMenuController;
    // [SerializeField] private GameObject _gameOverMenu;

    // [SerializeField] private Material _carMaterial;
    // [SerializeField] private Material _enemyMaterial;
    // [SerializeField] private Color _originalCar, _originalTurret;
    // [SerializeField] private Color _damageColor;
    // [SerializeField] private AudioSource _damageAudio;

    // //HealthThings
    // public int maxHealth = 100;
    // public float currentHealth = 100f;
    // private bool healingC = false;


    public Vector3 Velocity
    {
        get
        {
            if(_carRB == null)
            {
                _carRB = GetComponent<Rigidbody>();
            }
            return _carRB.velocity;
        }
    }

    #region Life Cycle

    private void Awake() 
    {
        _carRB = GetComponent<Rigidbody>();
        // _carMaterial.color = _originalCar;
        // _enemyMaterial.color = _originalTurret;

        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void FixedUpdate() 
    {
        // if(currentHealth <= 0)
        // {
        //     gameObject.SetActive(false);
        //     _gameOverMenu.SetActive(true);
        //     //Time.timeScale = 0;
        // }

        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
        MoveAbilities?.Invoke();
    }

    private void OnDestroy() 
    {
        GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
    }

    private void onGameStateChanged(GameState newGameState)
    {
        switch(newGameState)
        {
            case GameState.Gameplay:
            ResumeRigidbody();
            break;
            case GameState.Paused:
            PauseRigidbody();
            break;
            default:
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        // if(currentHealth > 0 && GameStateManager.instance.CurrentGameState == GameState.Gameplay && !healingC) StartCoroutine(HealCoroutine());
    }

    #endregion

    #region Private Methods

    private void ApplyEngineForce()
    {
        velocityUp = Vector3.Dot(transform.forward, _carRB.velocity);

        if(velocityUp > maxSpeed && accelerationInput > 0) return;
        if(velocityUp < -maxSpeed && accelerationInput < 0) return;  

        if(_carRB.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;

        if(accelerationInput == 0)
        {
            _carRB.drag = Mathf.Lerp(_carRB.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _carRB.drag = 0;
        }

        Vector3 engineForce = transform.forward * accelerationInput * accelerationFactor;
        _carRB.AddForce(engineForce, ForceMode.Force);
    }

    private void ApplySteering()
    {
        float minSpeed = (_carRB.velocity.magnitude / 8);
        minSpeed = Mathf.Clamp01(minSpeed);

        rotationAngle += steeringInput * turnFactor * minSpeed;

        Quaternion quat = Quaternion.Euler(0,rotationAngle,0);

        _carRB.MoveRotation(quat);
    }

    private void GetInput()
    {
        steeringInput = _joystick.Horizontal;
        //accelerationInput = 1;
    
        if(accelerationInput > 0)
        {
            backwards = false;
        }
        else
        {
            backwards = true;
        }
    }

    public void engineUP()
    {
        accelerationInput = 1;
    }

    public void engineBACK()
    {
        accelerationInput = -1;
    }

    public void frenada()
    {
        accelerationInput = 0;
    }

    private float GetLateralVelocity() 
    { 
        return Vector3.Dot(transform.right, _carRB.velocity); 
    }

    // private IEnumerator HealCoroutine()
    // {   
    //     if(!isCarDrifting(out float latVelocity, out bool isDrifting))
    //     {
    //         healingC = true;
    //         yield return new WaitForSeconds(1f);
    //         print("Healing.");
    //         currentHealth += 1f;
    //         health.value += 1f;
    //         currentHealth = health.value;
    //         healingC = false;
    //     }
    // }

    private void KillOrthogonalVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(_carRB.velocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(_carRB.velocity, transform.right);

        _carRB.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private void PauseRigidbody() 
    {
        Debug.Log("Pause with velocity=" + _carRB.velocity + " & angularVelocity=" + _carRB.angularVelocity);
        _pausedVelocity = _carRB.velocity;
        _pausedAngularVelocity = _carRB.angularVelocity;
        _carRB.isKinematic = true;
    }

    private void ResumeRigidbody() 
    {
        _carRB.isKinematic = false;
        _carRB.velocity = _pausedVelocity;
        _carRB.angularVelocity = _pausedAngularVelocity;
        Debug.Log("Resume with velocity=" + _carRB.velocity + " & angularVelocity=" + _carRB.angularVelocity);
    }

    // private IEnumerator takeDamageCoroutine()
    // {
    //     //Se cambia el color
    //     _carMaterial.color = _damageColor;
    //     _enemyMaterial.color = _damageColor;
    //     _damageAudio.Play();
    //     _mainMenuController.gameObject.transform.GetChild(0).gameObject.SetActive(false); // Radio
    //     yield return new WaitForSeconds(0.5f);
    //     _mainMenuController.gameObject.transform.GetChild(0).gameObject.SetActive(true); // Radio
    //     //Se vuelve a poner
    //     _carMaterial.color = _originalCar;
    //     _enemyMaterial.color = _originalTurret;
    // }

    #endregion

    #region Public Methods

    public bool isCarDrifting(out float latVelocity, out bool isDrifting)
    {
        latVelocity = GetLateralVelocity();
        isDrifting = false;

        if(accelerationInput < 0 && velocityUp > 0 && !backwards)
        {
            isDrifting = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 2.5f && !backwards)
            return true;
        
        return false;
    }

    public bool IsMoving()
    {
        return Mathf.Abs(_carRB.velocity.magnitude) > 0f; 
    }

    public void IncreaseSpeed(int amount)
    {
        accelerationFactor += amount;
        maxSpeed += amount;
    }

    // public void takeDamage(float value)
    // {
    //     cmscreenshake.ShakeCamera(8, 0.5f);
    //     health.value -= value;
    //     currentHealth -= value;
    //     print(health.value + " " + currentHealth);
    //     StartCoroutine(takeDamageCoroutine());
    // }

    

    #endregion

}
