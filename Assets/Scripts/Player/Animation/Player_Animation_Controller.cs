using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{
    [Header("Component Cache")]
    [SerializeField] private Animator playerAnimController;

    [Header("Animation Configs")]
    [SerializeField] private float secondsBeforeInvulnerabilityEnds = 3;
    

    // Animation Parameter Hash Values
    int playerIsDamagedParam_Hash;
    int startInvulnerabilityEndParam_Hash;

    // Internal Properties
    private int triggerToSet;
    // This is set every time an animation needs to play, and is then passed into the Animation Trigger Function
    // A property is used for selecting which animation to trigger so that no methods needing to be subscribed to a delegate for coroutine passing will need matching parameters.
  
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
        if(CoroutineFunction == null)
        {
            CoroutineFunction += AnimationTrigger;
        }

        TriggerToSet = playerIsDamagedParam_Hash; // Stage one of Damage: Slow Flash Invulnerability
        AnimationTrigger();

        TriggerToSet = startInvulnerabilityEndParam_Hash;
        StartCoroutine(TimeBeforeCodeExecution(secondsBeforeInvulnerabilityEnds, CoroutineFunction));
               
    }

    private void AnimationTrigger() => playerAnimController.SetTrigger(TriggerToSet);






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
