using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_RandomBouncer_Collisions : MonoBehaviour
{
    [SerializeField] private bool stayInPlayArea = false;
    [SerializeField] private int bouncesBeforeLeavingPlayArea;
    [SerializeField] private int currentBounces = 0;

    [Header("Component References")]
    [SerializeField] private static Collider2D playAreaColl;
    [SerializeField] private Collider2D thisEnemyCollider;

    public event UnityAction OnBounce;
    // Properties
    private int CurrentBounces
    {
        get { return currentBounces; }
        set
        {
            currentBounces = value;
            if (value >= BouncesBeforeLeavingPlayArea)
            {
                Physics2D.IgnoreCollision(thisEnemyCollider, playAreaColl); // Will allow the enemy to leave the area. Upon deactivating the enemy, this should reset.
            }
        }
    }
    private int BouncesBeforeLeavingPlayArea { get { return bouncesBeforeLeavingPlayArea; } set { bouncesBeforeLeavingPlayArea = value; } }

    private void Awake()
    {
        thisEnemyCollider = GetComponent<Collider2D>();
        if (playAreaColl == null)
        {
            playAreaColl = PlayAreaRefManager.PlayAreaBounds.GetComponent<Collider2D>(); // Will find edge collider. Could pass this reference in from object pool spawner so that every enemy doesnt have to
            // NEEDS WORK, currently the enemies can be set as each others playareacoll if they collider before reaching the play area.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (!stayInPlayArea)
        {
            
            stayInPlayArea=true;
           
            // make a getComponent reference call
            thisEnemyCollider.isTrigger = false;
           Debug.Log("Coll type: " + playAreaColl.GetType());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == playAreaColl)
        {
            CurrentBounces++;
          
           // Debug.Log(CurrentBounces + ": Current bounces");
        }
        OnBounce();
    }

    private void ResetCollisionLogic()
    {
        thisEnemyCollider.isTrigger = true;
        stayInPlayArea = false;
        currentBounces = 0;

    }

    private void OnDisable()
    {
        ResetCollisionLogic();
    }

    
}
