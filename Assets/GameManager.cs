using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        Debug.Log("OnPlayerHit Called from game manager script");
        if(currentGameSession.PlayerHP <= 0) { /* Game End Scenario Code */ }
        currentGameSession.PlayerHP--;
    }

  
}
