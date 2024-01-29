using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{
    [SerializeField] private Animator playerAnimController;
    int playerIsDamagedParamHash;
    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {
        playerAnimController = Static_Helper_Methods.FindComponentInGameObject<Animator>(gameObject);
        playerIsDamagedParamHash = Animator.StringToHash("PlayerIsDamaged");
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        Player_Collisions.playerTookDamage += TriggerDamageAnim;
    }

    private void TriggerDamageAnim()
    {
        Debug.LogError("Playing taken damage flash");
        playerAnimController.SetTrigger(playerIsDamagedParamHash);
    }
}
