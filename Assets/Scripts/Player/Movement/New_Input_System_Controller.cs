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
    [SerializeField] private float movementSpeed;
    [SerializeField] private float speedToAddOnBoost;
    [Range(0f, 1f)][SerializeField] private float fireRateDelay = 0.1f;
    private float boostValue = 0;

    [Header("Player Component Cache")]
    [SerializeField] private Rigidbody2D playerRB;


    // Fire Rate Control
    private float lastFireTime;
    bool canFire = true;

    
    public event UnityAction OnFireHit;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponentInParent<Rigidbody2D>();
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


    // Input System Events

    // Movement
    public void OnMove(InputValue value) // Input System Method
    {
        movementDirection = value.Get<Vector2>();
    }

    public void OnBoost() // Input System Method
    {
        boostValue = speedToAddOnBoost;
    }
    public void OnBoostRelease() // Input System Method
    {
        boostValue = 0;
    }

    // Actions
    public void OnFire() 
    {
        Debug.Log("PlayerFired");
         
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

    
    
    




}
