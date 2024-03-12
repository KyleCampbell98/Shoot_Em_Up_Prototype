using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    // Cached Component References
    private Collider2D playerCollider;

    // Internal Script logic
    private bool canCollideWithEnemies = true;
    private bool gameIsOver = false; // If true player can no longer collide with eneimes whilst the game plays behind the UI.

    public delegate void PlayerTookDamage();
    public static PlayerTookDamage m_playerTookDamage;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameManager.a_GameOver += StopCollisionsOnGameOver;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy_General_Collisions>() && canCollideWithEnemies) 
        {
            canCollideWithEnemies = false;
            Debug.Log("Player detected an enemy upon collision.");
            m_playerTookDamage();   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Shape_Projectile_Logic>())
        {
            Debug.Log("Player Collided with their own projectile.");
        }
    }

    public void ReinstateCanTakeDamage() // Work needed as player can still collide, probably being overwritten by the end of the player animation which reinstates collision with eneimes. 
    {
        if (gameIsOver) { return; }
  
        canCollideWithEnemies = true;
    }

    private void StopCollisionsOnGameOver()
    {
        Debug.Log("Stop Collision On Game Over called from PlayerCollisions");
        gameIsOver = true;
    }

    private void OnDisable()
    {
        GameManager.a_GameOver -= StopCollisionsOnGameOver;
    }
}
