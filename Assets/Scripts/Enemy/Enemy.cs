using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Base enemy class that initializes attributes shared by ALL enmeies. Enemies are setup by their spawners.
    /// 
    /// Attributes include: 
    /// generic component references,
    /// speed, target, shapeType (All set in the SetupEnemy Method, info provided by the enemyWaveSO)
    /// </summary>

   [Header("Component References")]
   [SerializeField] protected Rigidbody2D enemyRb;
   [SerializeField] protected SpriteRenderer enemySpriteRenderer;
   [SerializeField] protected GameObject topMostParentGameObjRef;
   [SerializeField] protected Animator enemyAnimationController;
   
   [Header("Generic Movement Configs")]
   [SerializeField] protected float movementSpeed;
   [SerializeField] protected GameObject movementTarget;

   [Header("Shape Identity Configs")]
   [SerializeField] protected Cutter_And_Enemy_Shape_Enums.ShapeType enemyShapeType;

   [Header("Spawn Data")]
   [SerializeField] protected Vector2 originSpawnPoint;

    protected virtual void Awake()
    {
        originSpawnPoint = transform.position;
    }
    
    protected virtual void LocateComponentReferences() // Overidden as subclasses may have unique components whilst calling this base method
    {
        if (enemyRb == null)
        {
            enemyRb = Static_Helper_Methods.FindComponentInGameObject<Rigidbody2D>(gameObject);
        }

        if (enemySpriteRenderer == null)
        {
            enemySpriteRenderer = Static_Helper_Methods.FindComponentInGameObject<SpriteRenderer>(gameObject);   
        }

        if(topMostParentGameObjRef == null)
        {
           topMostParentGameObjRef = Static_Helper_Methods.FindComponentInGameObject<ParentObjectIndicator>(gameObject).gameObject;
        }

        if(enemyAnimationController == null)
        {
            enemyAnimationController = Static_Helper_Methods.FindComponentInGameObject<Animator>(gameObject);
        }
    }
   
    public void SetUpEnemy(float waveMovementSpeed, Shape_Info shape_Info, GameObject enemyMovementTarget)
    {
        Debug.Log("SETUP CALLED");
        movementSpeed = waveMovementSpeed;
        enemyShapeType = shape_Info.ShapeType;
        enemySpriteRenderer.sprite = shape_Info.Sprite;
        movementTarget = enemyMovementTarget;

        enemyAnimationController.runtimeAnimatorController = shape_Info.ShapeAnimator;
        enemyAnimationController.Play("Entry");
    }

    protected virtual void EventSubscriptions() // Every enemy subclass must define its own set of Event subscriptions in order to function  
    {
        Enemy_General_Collisions egCollisionsComponent = Static_Helper_Methods.FindComponentInGameObject<Enemy_General_Collisions>(gameObject);
        egCollisionsComponent.collisionWithPlayerProjectile += OnCollisionWithPlayerProjectile; // Every enemy needs to have the ability to collide with a player projectile
    }
    protected void OnDisable()
    {
        ResetEnemyOnDisable();
    }

    protected virtual void ResetEnemyOnDisable()
    {
        transform.parent.position = originSpawnPoint;
    }

    // Generic Collision Logic

    protected void OnCollisionWithPlayerProjectile(Cutter_And_Enemy_Shape_Enums.ShapeType? collisionShapeType, GameObject projectileCollidedWith)
    {
        if (collisionShapeType == enemyShapeType)
        {
            Debug.Log("Upon Collision, both the enemy and player had matching enum types. ");
            GameManager.a_PlayerDefeatedEnemy();
            projectileCollidedWith.SetActive(false);
            topMostParentGameObjRef.SetActive(false);

        }
        else
        {
            Debug.Log("Collision logged, but shape types were mismatched");
        }
    } // This should really be in the "General Collisions" script, however that script currently has no access to shape type parameters.

}
