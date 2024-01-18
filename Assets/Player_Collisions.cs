using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    // Cached Component References
    private Collider2D playerCollider;

    public delegate void PlayerTookDamage();
    public static PlayerTookDamage playerTookDamage;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy_General_Collisions>()) 
        { 
            Debug.Log("Player detected an enemy upon collision."); 
        }
    }
}
