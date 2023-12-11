using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy_RandomBouncer : Enemy
{
    [Header("Movement Configs")]
    [SerializeField] private Vector2 movementDirection;
    [Header("Reference Cache")]
    [SerializeField] private GameObject testInstantiator;
    [SerializeField] private Collider2D playAreaBounds;
    [SerializeField] private bool canMoveTowardsTarget;
    // Unity Methods
    protected override void Awake()
    {
        base.Awake();
        EventSubscriptions();
        LocateComponentReferences();
        
    }
    private void Start()
    {
      SetAreaTarget(out bool targetSet);
        InitialMovementPush();
        canMoveTowardsTarget = targetSet;

    }

    private void OnEnable()
    {
        if (canMoveTowardsTarget)
        {
            InitialMovementPush();
        }
       
    }

    // Movement logic Methods
    private void InitialMovementPush()
    {
        Vector2 direction = RandomTargetGenerator() - (Vector2)transform.position; // Destination - source = direction
        enemyRb.velocity =  direction.normalized * movementSpeed;
    }
    private void SwitchDirection()
    {
        enemyRb.velocity = new Vector2(Mathf.Clamp(enemyRb.velocity.x, -10, 10), Mathf.Clamp(enemyRb.velocity.y, -10, 10));

        if (Math.Abs(enemyRb.velocity.y) <= 1)
        {
            enemyRb.AddForce(new Vector2(0, RandomForceGenerator()) * movementSpeed, ForceMode2D.Impulse);
        }

        if (Math.Abs(enemyRb.velocity.x) <= 1)
        {
            enemyRb.AddForce(new Vector2(RandomForceGenerator(), 0) * movementSpeed, ForceMode2D.Impulse);
        }

    }
    private int RandomForceGenerator() // Why static? 25/11/23 // Removed Static 27/11/28
    {
        var forceToAdd = UnityEngine.Random.Range(1, -2);
        if (forceToAdd == 0)
        {
            forceToAdd = 1;
        }
        Debug.Log("Array number randomly chosen: " + forceToAdd);
        return forceToAdd;
    }

    // Internal Script Utility
    private void EventSubscriptions()
    {
        if (GetComponentInParent<Enemy_RandomBouncer>())
        {
            GetComponentInParent<Enemy_RandomBouncer_Collisions>().OnBounce += SwitchDirection;
        }
        else if (GetComponent<Enemy_RandomBouncer>())
        {
            GetComponent<Enemy_RandomBouncer_Collisions>().OnBounce += SwitchDirection;
        }
        else { Debug.LogError("Component \"RandomBouncer Collisions\" not found within \"Random Bouncer Enemy\""); }
    }
    private void SetAreaTarget(out bool targetSuccessfullySet)
    {
        Debug.Log("Enemy Bouncer: \"Set Target\" called.");

        if (playAreaBounds == null)
        {
            if (movementTarget.GetComponent<Collider2D>() == null)
            {
                playAreaBounds = movementTarget.GetComponentInChildren<Collider2D>();
                Debug.Log($"Random Bouncer: playAreaBounds set to : {playAreaBounds.name}.\nFound in child component.");

            }
            else
            {
                playAreaBounds = movementTarget.GetComponent<Collider2D>();
                Debug.Log($"Random Bouncer: playAreaBounds set to : {playAreaBounds.name}.\nFound in parent component.");

            }
            targetSuccessfullySet = true;
        }
        else
        {
            targetSuccessfullySet = false;
        }
    }

    private Vector2 RandomTargetGenerator()
    {
        Vector2 newTarget;

        newTarget = new Vector2(UnityEngine.Random.Range(playAreaBounds.bounds.min.x, playAreaBounds.bounds.max.x),
            UnityEngine.Random.Range(playAreaBounds.bounds.min.y, playAreaBounds.bounds.max.y));
        Debug.Log($"New target destination for bouncer enemy: {newTarget}.");
        Instantiate(testInstantiator, newTarget, Quaternion.identity);

        return newTarget;
    }

    protected override void ResetEnemyOnDisable()
    {
        base.ResetEnemyOnDisable();
        
    }

}
