using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_RandomBouncer_Collisions : Enemy_General_Collisions
{
    [Header("Play Area Collision Control")]
    [SerializeField] private int bouncesBeforeLeavingPlayArea;
    [SerializeField] private int currentBounces = 0;

    [Header("Component References")]
    [SerializeField] private Collider2D playAreaColl; // Kept private to this class as other enemies that hunt the player won't need to be kept specifically confined to the play area.
   
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
                Physics2D.IgnoreCollision(thisEnemyCollider, playAreaColl); // Will allow the enemy to leave the area. Upon deactivating the enemy, this resets.
            }
        }
    }
    private int BouncesBeforeLeavingPlayArea { get { return bouncesBeforeLeavingPlayArea; } set { bouncesBeforeLeavingPlayArea = value; } }

    protected override void Awake()
    {
       base.Awake();
        if (playAreaColl == null)
        {
            Debug.Log("Play are collider assigned");
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
          
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision) 
    {
        base.OnCollisionEnter2D (collision);
        if(collision.collider == playAreaColl)
        {
            CurrentBounces++;
        }
        OnBounce();
        
    }

    protected override void ResetCollisionLogic()
    {
        base.ResetCollisionLogic();
        currentBounces = 0;

    } // Resets "Play Area Collision Control" on disable.

    

    
}
