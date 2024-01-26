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
        PlayerHP = healthStateSprites.Length;
        Array.Reverse(healthStateSprites);
       playerSpriteRenderer = Static_Helper_Methods.FindComponentInGameObject<SpriteRenderer>(this.gameObject);
        Player_Collisions.playerTookDamage += PlayerTookDamage;
    }





    private void PlayerTookDamage() 
    {
        Debug.Log("OnPlayerHit Called from game manager script");
        if ((PlayerHP - 1) <= 0) { /* Game End Scenario Code */ }
        PlayerHP--;
        PlayerHealthStateSprite();


    }

    private void PlayerHealthStateSprite()
    {
      // Need to implement a check for the player being dead so that the index doesnt go out of bounds.
     playerSpriteRenderer.sprite = healthStateSprites[playerHP];
    }
    

}
