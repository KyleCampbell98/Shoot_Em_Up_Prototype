using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wave_Push : MonoBehaviour
{
    private Collider2D wavePush_Collider;
    [SerializeField] private float maxWaveSize; // What the push circle will be by the end of the attack (in collider diameter)


    // Script Logic Control
    private bool isWavePushActive;

    // Start is called before the first frame update
    void Start()
    {
        wavePush_Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isWavePushActive)
        {
            WavePushAttack();
        }
    }

    private void OnEnable()
    {
        
    }

    private void WavePushAttack()
    {
        
    }

    private void OnDisable()
    {
        ResetWavePush();
    }

    private void ResetWavePush()
    {
      
    }
}
