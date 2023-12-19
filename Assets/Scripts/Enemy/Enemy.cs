using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
   [Header("Component References")]
   [SerializeField] protected Rigidbody2D enemyRb;
    [SerializeField] protected SpriteRenderer enemySpriteRenderer;
    
   [Header("Generic Movement Configs")]
   [SerializeField] protected float movementSpeed;
   [SerializeField] protected GameObject movementTarget;
   [SerializeField] protected bool collideWithPlayAreaBoundaries;
   [SerializeField] protected Cutter_And_Enemy_Shape_Enums.ShapeType enemyShapeType;

    [Header("Spawn Data")]
    [SerializeField] protected Vector2 originSpawnPoint;

    public GameObject MovementTarget
    {
       set { movementTarget = value; }
    }

    protected virtual void Awake()
    {
        originSpawnPoint = transform.position;
    }
    
    protected virtual void LocateComponentReferences() // Overidden as subclasses may have unique components whilst calling this base method
    {
        if (enemyRb == null)
        {
            if(gameObject.GetComponent<Rigidbody2D>() != null) 
            { 
                enemyRb = GetComponent<Rigidbody2D>();
                //Debug.Log("Enemy rigidbody found in self.");
            }
            else 
            { 
                enemyRb = GetComponentInParent<Rigidbody2D>();
                //Debug.Log("Enemy rigidbody found in parent game object.");

            }
        }
        if (enemySpriteRenderer == null)
        {
            if (gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>() != null)
            {
                enemySpriteRenderer = gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>();
            }
            else
            {
                Debug.LogError("Enemy Script: No Sprite Renderer found in other Enemy child game objects. COuld not Cache Reference.");   
            }
            //Debug.Log("Enemy rigidbody found in self."); 
        }
    }

    protected virtual void ResetEnemyOnDisable()
    {
        transform.parent.position = originSpawnPoint;
    }
    
    public void SetUpEnemy(float waveMovementSpeed, Cutter_And_Enemy_Shape_Enums.ShapeType waveEnemyShapeType, Sprite enemySprite, GameObject enemyMovementTarget)
    {
        movementSpeed = waveMovementSpeed;
        enemyShapeType = waveEnemyShapeType;
        enemySpriteRenderer.sprite = enemySprite;
        movementTarget = enemyMovementTarget;

        // NEED TO IMPLEMENT LOGIC
    }

    protected void OnDisable()
    {
        ResetEnemyOnDisable();
    }

    

}
