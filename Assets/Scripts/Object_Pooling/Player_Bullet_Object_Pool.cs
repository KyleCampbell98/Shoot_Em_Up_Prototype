using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Object_Pool : Object_Pool_Template
{
    [SerializeField] private Transform firePoint;
 

    private void Start()
    {
        SubscribeToFireEvent();
        PopulatePool(); // bullet pool
    }

    private void SubscribeToFireEvent()
    {
        if (gameObject.GetComponentInParent<New_Input_System_Controller>() != null)
        {
            this.GetComponentInParent<New_Input_System_Controller>().OnFireHit += ActivateBullets;
            //Debug.LogError("YEYE");
        }
        else { Debug.LogError("PARENT OBJECT MISSING CONTROLLER SCRIPT"); }
    }

    private void ActivateBullets()
    {
     //   Debug.Log("Activate Bullets in Player Object Pool Called");

        GameObject bullet = GetPooledObject();
        if(bullet != null)
        {
            bullet.transform.SetPositionAndRotation(firePoint.transform.position, transform.rotation.normalized);
            bullet.SetActive(true);
        }
        else { Debug.Log("Could not retrieve object to enable from pool."); }
    }

    private void OnDisable()
    {
     //   GetComponentInParent<New_Input_System_Controller>().OnFireHit -= ActivateBullets;
     ///* This code can't run as a disabled object can't retrieve a component.Need to have a separate function that
     ///unsubs from all events, before finalising the player destruction/disabling.
     ///*


    }
}
