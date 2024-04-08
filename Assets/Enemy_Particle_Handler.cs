using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Particle_Handler : MonoBehaviour
{
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem enemy_Destroyed_Particle;
   

    private Enemy_General_Collisions collisionsScriptRef;
    private void Awake()
    {
        if (Static_Helper_Methods.FindComponentInGameObject<Enemy_General_Collisions>(gameObject) != null)
        {
            collisionsScriptRef = Static_Helper_Methods.FindComponentInGameObject<Enemy_General_Collisions>(gameObject); // In theory should work as all enemy types should access the general collisions. 
          
        }
        else
        {
            Debug.Log("Enemy_Particle_Handler: Collision Script not found. Collision Events can't be listened to.");
        }
    }

   

    private void OnDisable()
    {
        Debug.Log("");
        enemy_Destroyed_Particle.Play();

    }

  
}
