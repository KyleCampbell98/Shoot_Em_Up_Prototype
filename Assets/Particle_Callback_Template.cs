using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Callback_Template : MonoBehaviour
{
    [SerializeField] private ParticleSystem idleEffect_Particle;

    protected void OnParticleSystemStopped()
    {
        Debug.Log("PARTICLE STOPPED");
        idleEffect_Particle.Play();
    }
}
