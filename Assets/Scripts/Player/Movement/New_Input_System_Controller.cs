using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class New_Input_System_Controller : MonoBehaviour
{
    [Header("Player Character Configs")]
    [SerializeField] private Vector2 movementDirection;
    [SerializeField] private float originalMovementSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementMultiplier = 1.25f;
    [SerializeField] private float movementMultiplierIncrement = 0.2f;
    [SerializeField] private float speedToAddOnBoost;
    [Range(0f, 1f)][SerializeField] private float fireRateDelay = 0.5f;
    [Range(2f, 30f)][SerializeField] private float emergencyPulseUseDelay = 20f; // Could potetnially add a UI element to tell players when the emergency pulse has cooled down. 
    private float boostValue = 0;

    [Header("Player Component Cache")]
    [SerializeField] private Rigidbody2D playerRB;

    public float EmergencyPulseUseDelay { get { return emergencyPulseUseDelay; } }
    bool isBoosting = false;
    private bool IsBoosting
    {
        get { return isBoosting; }
        set
        {
            isBoosting = value; if (value == true)
            {
                boostValue = speedToAddOnBoost;
            }
            else if (value == false) { boostValue = 0; }
        }
    }

    // Fire Rate Control
    private float lastFireTime;
    private float lastEmergencyPulseTime;
    bool canFire = true;
    bool canMove = true;
    bool canEmergencyPulse = true;

    // Emergency Wave Push Control

    
    public event UnityAction OnFireHit;
    public event UnityAction OnEmergencyPulseActivated;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponentInParent<Rigidbody2D>();
        GameManager.a_GameOver += StopPlayerControl;
        GameManager.a_spawnerRoundComplete += IncrementMovementSpeed;
        lastEmergencyPulseTime -= emergencyPulseUseDelay;
        originalMovementSpeed = movementSpeed;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Should probably move this into a separate script. Have THIS script as a means to purley capture input, to which a separate script would subscribe to an event to implement the input.
        playerRB.velocity = new Vector2(movementDirection.x * (movementSpeed + boostValue), movementDirection.y * (movementSpeed + boostValue)); // Setting velocity directly causes rigid movement, tight for the purpose of this game as twitch movement is needed.
    }

    private void IncrementMovementSpeed()
    {
        movementSpeed = originalMovementSpeed * movementMultiplier;
        movementMultiplier += movementMultiplierIncrement;
    }


    // Input System Events

    // Movement
    public void OnMove(InputValue value) // Input System Method
    {
       
        movementDirection = value.Get<Vector2>();
      //  movementDirection = new Vector2(Mathf.RoundToInt(movementDirection.x), Mathf.RoundToInt(movementDirection.y)).normalized;
    }

    public void OnBoost() // Input System Method
    {
       
          IsBoosting = true;
        
     
         
        
    }
    public void OnBoostRelease() // Input System Method
    {
        IsBoosting = false;
    }

    // Actions
    public void OnFire() 
    {     
        if (canFire)
        {
           float timeSinceLastFire = Time.time - lastFireTime;
            if (timeSinceLastFire >= fireRateDelay)
            {
                OnFireHit();
                lastFireTime = Time.time;
            }
           
        }
    } // Input System Method

    public void OnEmergencyPulse()
    {
        Debug.Log("On Emergency Pulse Top Level call");

        if (canEmergencyPulse)
        {
            float timeSinceLastPulse = Time.time - lastEmergencyPulseTime;
            if(timeSinceLastPulse >= emergencyPulseUseDelay)
            {
                Debug.Log("On Emergency Pulse Final Method Level call");
                OnEmergencyPulseActivated();
                lastEmergencyPulseTime = Time.time;
            }
        }
    }

    public void OnPause_Unpause()
    {
       GameManager.a_ActivatePause();
    }
        
    private void StopPlayerControl()
    {
        Debug.Log("Stop Player Control Called");
       movementSpeed = 0;
    }

    private void OnDisable()
    {
        GameManager.a_GameOver -= StopPlayerControl;
        GameManager.a_spawnerRoundComplete -= IncrementMovementSpeed;
    }

}

    
    
    





