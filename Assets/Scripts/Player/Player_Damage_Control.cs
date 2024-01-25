using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage_Control : MonoBehaviour
{
    [Header("Reference Cache")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    
    [Header("Player Stats")]
    [SerializeField] private int playerHP;
    // Player stats properties 
    public int PlayerHP { get { return playerHP; } set { playerHP = value; } } // Needs to be assigned once hit

    // Start is called before the first frame update
    void Start()
    {
       playerSpriteRenderer = FindComponentInGameObject<SpriteRenderer>();
    }





    private void PlayerTookDamage() 
    {
        Debug.Log("OnPlayerHit Called from game manager script");
        if ((PlayerHP - 1) <= 0) { /* Game End Scenario Code */ }
        PlayerHP--;
    }

    private T FindComponentInGameObject<T>() where T : Component // Needs to go into a static helper class
    {
       
            Debug.Log("GENERIC METHOD STARTED");
            if (GetComponent<T>() != null)
            {
                Debug.Log("GENERIC METHOD IS BEING CALLED: " + GetComponent<T>().GetType().ToString());
                return GetComponent<T>();
            }
            else if (GetComponentInParent<T>() != null)
            {
                return GetComponentInParent<T>();
            }
            else if (GetComponentInChildren<T>() != null)
            {
                return GetComponentInChildren<T>();
            }
        return null;

        
    }
}
