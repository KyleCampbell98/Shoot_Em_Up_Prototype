using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Wave_Push : MonoBehaviour
{
    [SerializeField] private CircleCollider2D player_Collider; // Reference to the player coll to ignore collisions so that the push doesnt affect the player. Assigned in inspector
    [SerializeField] private New_Input_System_Controller playerLogicScript;
    private CircleCollider2D wavePush_Collider;

    [Header("Wave Push Configs")]
    [SerializeField] private float wavePushSpeed;
    [SerializeField] private float leprDuration;
    [SerializeField] private float maxWaveSize; // What the push circle will be by the end of the attack (in collider diameter)

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        EventSubscriptions();

        wavePush_Collider.radius = 0;
        ColliderPhysicsSetup();

    }

    private void EventSubscriptions()

    }

    private void EventSubscriptions()
    {
        playerLogicScript.OnEmergencyPulseActivated += EnableWavePush;
    }

    private void ColliderPhysicsSetup()
    {
        Physics2D.IgnoreCollision(wavePush_Collider, PlayAreaRefManager.PlayAreaBounds.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player_Collider, wavePush_Collider);
    }

    private void GetReferences()
    {
        wavePush_Collider = GetComponent<CircleCollider2D>();
        playerLogicScript = Static_Helper_Methods.FindComponentInGameObject<New_Input_System_Controller>(gameObject);
        if (playerLogicScript == null) { Debug.Log("Player Wave Push: Player logic script not found"); }
    }

    private void Update()

    {
        playerLogicScript.OnEmergencyPulseActivated += EnableWavePush;
    }

    private void ColliderPhysicsSetup()
    {

        Physics2D.IgnoreCollision(wavePush_Collider, PlayAreaRefManager.PlayAreaBounds.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player_Collider, wavePush_Collider);

        Debug.Log("Wave Push Attack Called");
        if (Mathf.Approximately(maxWaveSize, wavePush_Collider.radius)) 
        {
            ResetWavePush();
            return;
        }
        leprDuration += Time.deltaTime;
        wavePush_Collider.radius = Mathf.Lerp(wavePush_Collider.radius, maxWaveSize,  leprDuration / wavePushSpeed); // Further Lerping Study Required: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity

    }

    private void GetReferences()
    {
        wavePush_Collider = GetComponent<CircleCollider2D>();
        playerLogicScript = Static_Helper_Methods.FindComponentInGameObject<New_Input_System_Controller>(gameObject);
        if (playerLogicScript == null) { Debug.Log("Player Wave Push: Player logic script not found"); }
    }


    private void EnableWavePush()
    {


        // isWavePushActive = true;

        StartCoroutine(WavePushEG());
    }

    private void ResetWavePush()
    {
        Debug.Log("Player Wave Push: Reset Wave Push Called");
        
        wavePush_Collider.radius = 0;
    }

    /// <summary>
    /// Aid with this method's functionality came from https://discussions.unity.com/t/how-to-prevent-lerp-from-slowing-down/57065/3
    /// Further Lerping Study Required: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/#how_to_use_lerp_in_unity
    /// </summary>
    /// <returns></returns>
    

    private IEnumerator WavePushEG() 
    {
        float timeElapsed = 0;

        while (timeElapsed < leprDuration && !Mathf.Approximately(maxWaveSize, wavePush_Collider.radius))
        {
            Debug.Log("Coroutine actually ran");
            wavePush_Collider.radius = Mathf.Lerp(wavePush_Collider.radius, maxWaveSize, timeElapsed /  wavePushSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

       
        ResetWavePush();
    }

    private void OnDisable()
    {
        playerLogicScript.OnEmergencyPulseActivated -= EnableWavePush;

    }



   private IEnumerator WavePushEG()
    {
        float timeElapsed = 0;

        while (timeElapsed < leprDuration && !Mathf.Approximately(maxWaveSize, wavePush_Collider.radius))
        {
            Debug.Log("Coroutine actually ran");
            wavePush_Collider.radius = Mathf.Lerp(wavePush_Collider.radius, maxWaveSize, timeElapsed /  wavePushSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

       
        ResetWavePush();
    }

}
