using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Area_Collision_Manager : MonoBehaviour
{
   
   [SerializeField] private ParticleSystem edge_Collision_Particle;

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 particleSpawnPoint = collision.GetContact(0).point;
        
        
        
        Instantiate(edge_Collision_Particle, particleSpawnPoint, Quaternion.identity);
    }
}
