using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wave_Push : MonoBehaviour
{
    [SerializeField] private CircleCollider2D player_Collider; // Reference to the player coll to ignore collisions so that the push doesnt affect the player. Assigned in inspector
    [SerializeField] private New_Input_System_Controller playerLogicScript;
    private CircleCollider2D wavePush_Collider;

    [Header("Wave Push Configs")]
    [SerializeField] private float wavePushSpeed;
    private float timeLerped;
    [SerializeField] private float maxWaveSize; // What the push circle will be by the end of the attack (in collider diameter)


    // Script Logic Control
    [SerializeField] private bool isWavePushActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerLogicScript = Static_Helper_Methods.FindComponentInGameObject<New_Input_System_Controller>(gameObject);
        if(playerLogicScript == null ) { Debug.Log("Player Wave Push: Player logic script not found"); }
        playerLogicScript.OnEmergencyPulseActivated += EnableWavePush;
        wavePush_Collider = GetComponent<CircleCollider2D>();
        wavePush_Collider.radius = 0;
        Physics2D.IgnoreCollision(wavePush_Collider, PlayAreaRefManager.PlayAreaBounds.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player_Collider, wavePush_Collider);
    }

    private void Update()
    {
        if (isWavePushActive)
        {
            WavePushAttack();
        }
    }

    private void WavePushAttack() // Aid with this method's functionality came from https://discussions.unity.com/t/how-to-prevent-lerp-from-slowing-down/57065/3
    {
        Debug.Log("Wave Push Attack Called");
        if (Mathf.Approximately(maxWaveSize, wavePush_Collider.radius)) 
        {
            ResetWavePush();
            return;
        }
        timeLerped += Time.deltaTime;
        wavePush_Collider.radius = Mathf.Lerp(wavePush_Collider.radius, maxWaveSize,  timeLerped / wavePushSpeed); // Further Lerping Study Required: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity
    }

    private void EnableWavePush()
    {
        isWavePushActive = true;
    }

    private void ResetWavePush()
    {
        Debug.Log("Player Wave Push: Reset Wave Push Called");
        timeLerped = 0;
        wavePush_Collider.radius = 0;
        isWavePushActive = false;
    }

    private void OnDisable()
    {
        playerLogicScript.OnEmergencyPulseActivated -= EnableWavePush;

    }

}
