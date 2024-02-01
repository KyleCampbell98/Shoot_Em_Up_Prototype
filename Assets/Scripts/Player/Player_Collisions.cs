using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    // Cached Component References
    private Collider2D playerCollider;

    // Internal Script logic
    private bool canCollideWithEnemies = true;

    public delegate void PlayerTookDamage();
    public static PlayerTookDamage m_playerTookDamage;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
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

    public void ReinstateCanTakeDamage()
    {
        Debug.LogError("REINSTATE DAMAGE CALLED");
        canCollideWithEnemies = true;
    }
}
