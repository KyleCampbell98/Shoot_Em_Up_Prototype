using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Particle_Handler : MonoBehaviour
{
    [Header("Parenting Cache")]
    [SerializeField] Transform instantiatedParticleParent;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem enemy_Destroyed_Particle;
   

    private Enemy_General_Collisions collisionsScriptRef;

    private void Awake()
    {
        instantiatedParticleParent = FindAnyObjectByType<Instantiated_Particle_Parent>().transform;
    }

    public void SpawnDeathParticle()
    {
        Instantiate(enemy_Destroyed_Particle, transform.position, Quaternion.identity, instantiatedParticleParent);
    }





}
