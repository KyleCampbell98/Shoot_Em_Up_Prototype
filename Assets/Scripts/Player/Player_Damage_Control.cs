using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage_Control : MonoBehaviour
{
    [Header("Reference Cache")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;    
    [SerializeField] private Sprite[] healthStateSprites;
    
    [Header("Player Stats")]
    [SerializeField] private int playerHP;
    // Player stats properties 
    public int PlayerHP { get { return playerHP; } set { playerHP = value; } } // Needs to be assigned once hit

    // Start is called before the first frame update
    void Start()
    {
        SetupPlayerHealth();
        Player_Collisions.playerTookDamage += PlayerTookDamage;
    }

    private void SetupPlayerHealth()
    {
        PlayerHP = healthStateSprites.Length - 1;
        Array.Reverse(healthStateSprites); // Array is reversed due to the order that the sprites were added to the array
        playerSpriteRenderer = Static_Helper_Methods.FindComponentInGameObject<SpriteRenderer>(gameObject);
        playerSpriteRenderer.sprite = healthStateSprites[healthStateSprites.Length - 1];
    }

    private void PlayerTookDamage() 
    {
        Debug.Log("OnPlayerHit Called from game manager script");
        if ((PlayerHP) <= 0) { Debug.Log(" Game End Scenario Code "); return; }
        PlayerHP--;
        PlayerHealthStateSprite();


    }

    private void PlayerHealthStateSprite()
    {
       // if ((PlayerHP < 0)) { Debug.Log("Final Health State already achieved. This is where the player would die"); return; }
        // Need to implement a check for the player being dead so that the index doesnt go out of bounds.
        playerSpriteRenderer.sprite = healthStateSprites[PlayerHP];
    }
    

}
