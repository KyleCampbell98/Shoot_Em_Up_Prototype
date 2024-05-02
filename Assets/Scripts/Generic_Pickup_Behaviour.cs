using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generic_Pickup_Behaviour : MonoBehaviour
{
    [SerializeField] protected float pickupLifespan; // How long the pickup has before despawning.
    [SerializeField] protected ParticleSystem idleEffect_Particle;
    [SerializeField] ParticleSystem itemSpawn_Particle;
    bool isInstantiated;

    private void Awake()
    {
        var main = itemSpawn_Particle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    private void OnEnable()
    {
        if (!isInstantiated) { return; }
        Invoke("DisablePickup", pickupLifespan);
    }

    private void OnDisable()
    {
        if (!isInstantiated) { isInstantiated = true; return; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerRefManager>())
        {
            Debug.Log("Collided with a player, disabling Invoke");
            DisablePickup();
        }
    }

    protected virtual void DisablePickup()
    {
        Debug.Log("Generic Pickup Behaviour: Disable Pickup Called");
        CancelInvoke();
        transform.position = transform.parent.position;
    }
}
