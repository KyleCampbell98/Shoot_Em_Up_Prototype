using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Pickup_Behaviour : MonoBehaviour
{
    [SerializeField] protected float pickupLifespan; // How long the pickup has before despawning.
    [SerializeField] ParticleSystem idleParticleEffect;
    private void Start()
    {
       idleParticleEffect.Play();
        Invoke("DisablePickup", pickupLifespan);
    }

    private void OnDisable()
    {
        DisablePickup();
        CancelInvoke();
        transform.position = transform.parent.position;
       
    }

    protected virtual void DisablePickup()
    {
        
    }
}
