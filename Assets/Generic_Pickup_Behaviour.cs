using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Pickup_Behaviour : MonoBehaviour
{
    private void OnDisable()
    {
        transform.position = transform.parent.position;
    }
}
