using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Detector_Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(nameof(Trigger_Detector_Test) + ": Detetced. Trigger source: " + collision.gameObject.name);
    }
}
