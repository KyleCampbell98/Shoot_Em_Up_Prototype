using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Environment_Controller : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform thisTransform;
    [SerializeField] bool canTurnOctogon;

    // Octogon Movement Info
    private Vector2 OctogonMovementDirection { get; set; }
  
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        TurnOctogon();
    }

    private void TurnOctogon()
    {
        switch (canTurnOctogon)
        {
            case true:
             //   Debug.Log("Turn Octogon being called");
                thisTransform.Rotate(Vector3.forward, OctogonMovementDirection.x * rotationSpeed * Time.fixedDeltaTime);
                break;
            default:
                return;
                
        }
        
    }

    public void OnTurnOctogon(InputValue inputValue) // Input System Method
    {
        if (inputValue.Get<Vector2>() == Vector2.zero) { canTurnOctogon = false; }
        else {
            canTurnOctogon = true;
           // Debug.Log("Turn Octogon Detected");
            OctogonMovementDirection = inputValue.Get<Vector2>();
        }
        
        
    }




}
