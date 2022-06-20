using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class CarController : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _accelerateButton;

    private Rigidbody _carRB;
    
    // [SerializeField] private float accelerationFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    // [SerializeField] private float maxSpeed = 20f;
    [SerializeField] float driftFactor = 0.95f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityUp = 0;
    private bool backwards = false;

    // Pausa
    private Vector3 _pausedVelocity;
    private Vector3 _pausedAngularVelocity;

    private float _timeStuck;
    private Coroutine _unstuckCoroutine;

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
        _carRB = gameObject.GetComponent<Rigidbody>();
        _joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        _accelerateButton = GameObject.FindGameObjectWithTag("AccelerateButton").GetComponent<Button>();
        GameStateManager.instance.onGameStateChanged += onGameStateChanged;
    }

    private void FixedUpdate() 
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
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
    }

    #endregion

    #region Private Methods

    private void ApplyEngineForce()
    {
        velocityUp = Vector3.Dot(transform.forward, _carRB.velocity);

        if(velocityUp > PlayerStats.instance.maxSpeed && accelerationInput > 0) return;
        if(velocityUp < -PlayerStats.instance.maxSpeed && accelerationInput < 0) return;  

        if(_carRB.velocity.sqrMagnitude > PlayerStats.instance.maxSpeed * PlayerStats.instance.maxSpeed && accelerationInput > 0) return;

        if(accelerationInput == 0)
        {
            _carRB.drag = Mathf.Lerp(_carRB.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _carRB.drag = 0;
        }

        Vector3 engineForce = transform.forward * accelerationInput * PlayerStats.instance.accelerationFactor;
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
        _carRB.angularVelocity = Vector3.zero;
    }

    private float GetLateralVelocity() 
    { 
        return Vector3.Dot(transform.right, _carRB.velocity); 
    }

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
        PlayerStats.instance.accelerationFactor += amount;
        PlayerStats.instance.maxSpeed += amount;
    }    

    #endregion

    // private void OnCollisionStay(Collision other) 
    // {
    //     if(other.gameObject.tag != "Wall") return;

    //     _timeStuck += Time.deltaTime;  
    //     if(_timeStuck > 0.75f)
    //     {
    //         if(_unstuckCoroutine == null)
    //         {
    //             _unstuckCoroutine = StartCoroutine(crUnstuck());
    //         }
    //     }  
    // }

    // private void OnCollisionExit(Collision other) 
    // {
    //     Debug.Log("SALE DE STUCK");
    //     _timeStuck = 0f;    
    // }

    // private IEnumerator crUnstuck()
    // {
    //     Vector3 engineForce = (Vector3.zero-transform.position) * 20f;
    //     _carRB.AddForce(engineForce, ForceMode.Force);
    //     yield return new WaitForSeconds(1f);
    //     _carRB.velocity = Vector3.zero;
    //     _unstuckCoroutine = null;
    // }

}
