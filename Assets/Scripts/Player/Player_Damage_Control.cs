using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage_Control : MonoBehaviour
{
    [Header("Reference Cache")]
    [SerializeField] private InPlay_Details gameSessionsSO;
    // Player stats properties 

    private void PlayerTookDamage() 
    {
        
        Debug.Log("OnPlayerHit Called from game manager script");
        if ((gameSessionsSO.PlayerHP) <= 0) 
        {
            GameManager.a_GameOver();
       
            Debug.Log(" Game End Scenario Code "); 
            return; 
        }
        gameSessionsSO.PlayerHP--;

    } 

    private void OnDisable()
    {
        Player_Collisions.m_playerTookDamage -= PlayerTookDamage;
    }


}
