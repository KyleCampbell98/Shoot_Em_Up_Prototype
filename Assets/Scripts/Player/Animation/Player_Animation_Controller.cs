using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{
    [Header("Component Cache")]
    [SerializeField] private Animator playerAnimController;

    [Header("Test Bool")]
    [SerializeField] private bool testworks = true;

    // Animation Parameter Hash Values
    int playerIsDamagedParam_Hash;
    int startInvulnerabilityEndParam_Hash;

    // Internal Properties
    private int triggerToSet;
    private int TriggerToSet { get { return triggerToSet; } set { triggerToSet = value;  }  }

    // Internal Delegates
    private event Action CoroutineFunction;
    

    void Start()
    {
        LocateComponents();
        ConvertStringParamsToHash();
        EventSubscriptions();
    }

  
   
    private void TriggerDamageAnim()
    {
        Debug.LogError("TriggerDamageAnim triggered from Player Anim Controller");
        if(CoroutineFunction == null)
        {
            CoroutineFunction += AnimationTrigger;
        }
        TriggerToSet = playerIsDamagedParam_Hash;
        AnimationTrigger();
        TriggerToSet = startInvulnerabilityEndParam_Hash;
        StartCoroutine(TimeBeforeCodeExecution(3,  CoroutineFunction));
        
       
            
    }

    private void AnimationTrigger()
    {
        Debug.LogError("TRIGGER TO SET IS NOW: " + TriggerToSet.ToString());
        playerAnimController.SetTrigger(TriggerToSet);

    }

    private IEnumerator TimeBeforeCodeExecution(float timeToWait, Action functionToDelay )
    {
        yield return new WaitForSeconds(timeToWait);
        functionToDelay.Invoke();
     
      
    }

    // Internal Script Logic
    private void LocateComponents()
    {
        playerAnimController = Static_Helper_Methods.FindComponentInGameObject<Animator>(gameObject);
    }

    private void ConvertStringParamsToHash()
    {
        playerIsDamagedParam_Hash = Animator.StringToHash("PlayerIsDamaged");
        startInvulnerabilityEndParam_Hash = Animator.StringToHash("StartInvulnerabilityEnd");
    }

    private void EventSubscriptions()
    {
        Player_Collisions.m_playerTookDamage += TriggerDamageAnim;
    }
}
