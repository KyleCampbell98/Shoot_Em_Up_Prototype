using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup_Ref : Generic_Pickup_Behaviour
{

    protected override void DisablePickup()
    {
        if (GameManager.health_Pickup_Is_Active) { GameManager.health_Pickup_Is_Active = false; }
        base.DisablePickup();
        Pickup_Slider_Controller.a_ResetSlider();
        gameObject.SetActive(false);
    }

  

}
