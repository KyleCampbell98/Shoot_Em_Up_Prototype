using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState { InPlay, Paused} // Potentially needs to be expanded on. Only implemented for now as a reminder that some form of state management is needed. 

    

    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;

    private float timeSurvived;

    private void Start()
    {
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        Player_Collisions.playerTookDamage += OnPlayerHit;
    }

    private void OnPlayerHit()
    {
       
    }

  
}
