using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Particle_Controller : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerDamagedSparks_Particle;
    [SerializeField] private ParticleSystem playerEmergencyWave_Particle;

    // Start is called before the first frame update
    void Start()
    {
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        Player_Collisions.m_playerCollisionsEvent += TriggerDamagedEffect; }

    private void TriggerDamagedEffect(bool damaged)
    {
        if(damaged)
        {
            playerDamagedSparks_Particle.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
