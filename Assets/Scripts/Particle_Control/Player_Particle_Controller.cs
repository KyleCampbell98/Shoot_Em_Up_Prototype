using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Particle_Controller : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem playerDamagedSparks_Particle;
    [SerializeField] private ParticleSystem playerEmergencyWave_Particle;
    [SerializeField] private ParticleSystem playerHealthPickup_Particle;

    [Header("Script Refs")]
    [SerializeField] private New_Input_System_Controller playerLogicScript;

    // Start is called before the first frame update
    void Start()
    {
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        Player_Collisions.m_playerCollisionsEvent += PLayerCollisionParticleHandler;
        playerLogicScript = Static_Helper_Methods.FindComponentInGameObject<New_Input_System_Controller>(gameObject);
        playerLogicScript.OnEmergencyPulseActivated += TriggerEmergencyPulseEffect;
    }

    private void PLayerCollisionParticleHandler(bool damaged)
    {
        if(damaged)
        {
            playerDamagedSparks_Particle.Play();
        }
        else
        {
            playerHealthPickup_Particle.Play();
        }
    }

    private void TriggerEmergencyPulseEffect()
    {
        playerEmergencyWave_Particle.Play();
    }

  

    private void OnDisable()
    {
        Player_Collisions.m_playerCollisionsEvent -= PLayerCollisionParticleHandler;
        playerLogicScript.OnEmergencyPulseActivated -= TriggerEmergencyPulseEffect;

    }
}
