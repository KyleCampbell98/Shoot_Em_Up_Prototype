using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawn_Particle_Control : Particle_Callback_Template
{
    [SerializeField] private ParticleSystem healthPickupIdle_Particle;

    protected override void OnParticleSystemStopped()
    {
        healthPickupIdle_Particle.Play();
    }
}
