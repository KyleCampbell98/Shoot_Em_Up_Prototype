using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_General_Collisions : MonoBehaviour
{
    [Header("Play Area Collision Control")]
    [SerializeField] protected bool stayInPlayArea = false;

    [Header("Component References")]
    [SerializeField] protected Collider2D thisEnemyCollider;

    //Events/Delegates
    public delegate void CollideWithPlayerProjectile(Cutter_And_Enemy_Shape_Enums.ShapeType? playerProjectileShapeType, GameObject projectileCollidedWith); // pass game object so that if enums match, GO reference can be used for transform details for animation
    public CollideWithPlayerProjectile collisionWithPlayerProjectile;

    protected virtual void Awake()
    {
        thisEnemyCollider = GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<Player_Shape_Projectile_Logic>())
        {
            if (collision.gameObject.GetComponent<Player_Shape_Projectile_Logic>())
            {
                collisionWithPlayerProjectile(collision.gameObject.GetComponent<Player_Shape_Projectile_Logic>().ProjectilesShapeType, collision.gameObject);
            }
          
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        /* 
         * Code that belongs to all Enemy collisions:
         * Sound to play on collision
         * Affect player (Health, kickback, any potential interactive elements)
         */
    }

    protected virtual void ResetCollisionLogic()
    {
        thisEnemyCollider.isTrigger = true;
        stayInPlayArea = false;
        
    } // Resets "Play Area Collision Control" on disable.

    protected void OnDisable()
    {
        ResetCollisionLogic();
    }
}
