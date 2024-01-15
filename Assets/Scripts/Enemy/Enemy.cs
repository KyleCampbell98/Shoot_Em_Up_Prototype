using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
   [Header("Component References")]
   [SerializeField] protected Rigidbody2D enemyRb;
   [SerializeField] protected SpriteRenderer enemySpriteRenderer;
   [SerializeField] protected GameObject topMostParentGameObjRef;
    
   [Header("Generic Movement Configs")]
   [SerializeField] protected float movementSpeed;
   [SerializeField] protected GameObject movementTarget;
   [SerializeField] protected bool collideWithPlayAreaBoundaries;
   [SerializeField] protected Cutter_And_Enemy_Shape_Enums.ShapeType enemyShapeType;

    [Header("Spawn Data")]
    [SerializeField] protected Vector2 originSpawnPoint;

    public GameObject MovementTarget
    {
       set { movementTarget = value; Debug.Log("Setting movement target in general enemy script."); } // Could do a null check, and only set if the target is null (Extra precaution for public variable)
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
                Debug.Log("Im reaching here at least");
                enemySpriteRenderer = gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>();
            }
            else
            {
                Debug.LogError("Enemy Script: No Sprite Renderer found in other Enemy child game objects. COuld not Cache Reference.");   
            }
            //Debug.Log("Enemy rigidbody found in self."); 
        }
        if(topMostParentGameObjRef == null)
        {
            if(GetComponent<ParentObjectIndicator>() != null) 
            { 
                topMostParentGameObjRef = this.gameObject; 
            }
            else if(GetComponentInParent<ParentObjectIndicator>() != null)
            {
                topMostParentGameObjRef = GetComponentInParent<ParentObjectIndicator>().gameObject;
            }
            else { topMostParentGameObjRef = gameObject.transform.parent.GetComponentInChildren<ParentObjectIndicator>().gameObject; }
        }
    }

    protected virtual void ResetEnemyOnDisable()
    {
        transform.parent.position = originSpawnPoint;
    }
    
    public void SetUpEnemy(float waveMovementSpeed, Shape_Info shape_Info, GameObject enemyMovementTarget)
    {
        movementSpeed = waveMovementSpeed;
        enemyShapeType = shape_Info.ShapeType;
        enemySpriteRenderer.sprite = shape_Info.Sprite;
        movementTarget = enemyMovementTarget;

        
    }

    protected abstract void EventSubscriptions(); // Every enemy subclass must define its own set of Event subscriptions in order to function  

    protected void OnDisable()
    {
        ResetEnemyOnDisable();
    }

    protected void OnCollisionWithPlayerProjectile(Cutter_And_Enemy_Shape_Enums.ShapeType? collisionShapeType, GameObject projectileCollidedWith)
    {
        if (collisionShapeType == enemyShapeType)
        {
            Debug.Log("Upon Collision, both the enemy and player had matching enum types. ");
            projectileCollidedWith.SetActive(false);
            topMostParentGameObjRef.SetActive(false);

        }
        else
        {
            Debug.Log("Collision logged, but shape types were mismatched");
        }
    }

}
