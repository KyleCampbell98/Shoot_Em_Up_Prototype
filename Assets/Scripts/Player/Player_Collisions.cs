using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Collisions : MonoBehaviour
{
    // Cached Component References
    private Collider2D playerCollider;

    // Internal Script logic
    private bool canCollideWithEnemies = true;
    private bool gameIsOver = false; // If true player can no longer collide with eneimes whilst the game plays behind the UI.

    public delegate void PlayerCollsionsEvent(bool damaged);
    public static PlayerCollsionsEvent m_playerCollisionsEvent; // Mainly handles player animations
    public UnityEvent m_playerTookDamageEvent;

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
            Debug.Log("Player Collisions: Player detected an enemy upon collision.");
     
            m_playerCollisionsEvent(damaged : true);   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool pickupIsHealth = true;
        if (canCollideWithEnemies)
        {
            if (collision.GetComponent<Player_Shape_Projectile_Logic>())
            {
                Debug.Log("Player Collisions: Player Collided with their own projectile.");
            }
            else if (collision.GetComponent<Health_Pickup_Ref>())
            {
                pickupIsHealth = true;

                GameManager.a_PlayerCollectedPickup(pickupIsHealth);
                m_playerCollisionsEvent(damaged: false);
            }
            else if (collision.GetComponent<Bomb_Pickup_Ref>())
            {
                pickupIsHealth = false;
                GameManager.a_PlayerCollectedPickup(pickupIsHealth);
            }

            collision.gameObject.SetActive(false);
        }
    }

    public void ReinstateCanTakeDamage() // Work needed as player can still collide, probably being overwritten by the end of the player animation which reinstates collision with eneimes. 
    {
        if (gameIsOver) { return; }
  
        canCollideWithEnemies = true;
    }

    private void StopCollisionsOnGameOver()
    {
        Debug.Log("Player Collisions: Stop Collision On Game Over called.");
        gameIsOver = true;
    }

    private void OnDisable()
    {
        GameManager.a_GameOver -= StopCollisionsOnGameOver;
    }
}
