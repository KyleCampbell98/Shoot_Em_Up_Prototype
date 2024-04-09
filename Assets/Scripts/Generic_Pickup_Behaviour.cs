using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generic_Pickup_Behaviour : MonoBehaviour
{
    [SerializeField] protected float pickupLifespan; // How long the pickup has before despawning.
    [SerializeField] protected ParticleSystem idleEffect_Particle;
    [SerializeField] ParticleSystem itemSpawn_Particle;

    private void Awake()
    {
        var main = itemSpawn_Particle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    private void OnEnable()
    {      
        Invoke("DisablePickup", pickupLifespan);
    }

    private void OnDisable()
    {
        
       Pickup_Slider_Controller.a_ResetSlider();
    }

    

    protected virtual void DisablePickup()
    {
        Debug.Log("Generic Pickup Behaviour: Disable Pickup Called");
        CancelInvoke();
        transform.position = transform.parent.position;
    }
}
