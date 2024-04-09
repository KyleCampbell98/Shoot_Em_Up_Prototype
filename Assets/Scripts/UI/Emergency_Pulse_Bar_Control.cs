using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Emergency_Pulse_Bar_Control : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private New_Input_System_Controller playerInputController_Script;

    [Header("Component References")]
    [SerializeField] private Slider emergencyPulse_Slider;
    [SerializeField] private Animator empProgressBar_Animator;

    // Progress Bar Configs
    private float barTotaldivider; // The number that the overall bar total will be divided by. EG: Bar takes 20 seconds to reacharge, divider = 20; 

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        EventSubscriptions();
        InitialBarSetup();
    }

   

    private void UpdateRechargeBar()
    {

        emergencyPulse_Slider.value = 0;


        StartCoroutine(RechargeBar());
    }

    private IEnumerator RechargeBar()
    {

        float timeElasped = 0f;
        empProgressBar_Animator.SetBool("Enemy_Goal_Hit", false);
        while (timeElasped < barTotaldivider)
        {
            Debug.Log("Emergency_Pulse_Slider_Control: Recharge Bar Coroutine Activated");
            emergencyPulse_Slider.value = Mathf.Lerp(emergencyPulse_Slider.minValue, emergencyPulse_Slider.maxValue, timeElasped / barTotaldivider);
            timeElasped += Time.deltaTime;
            yield return null;
        }

        empProgressBar_Animator.SetBool("Enemy_Goal_Hit", true);

    }

    // Internal Script Logic
    private void EventSubscriptions()
    {
        playerInputController_Script.OnEmergencyPulseActivated += UpdateRechargeBar;
    }

    private void InitialBarSetup()
    {
        barTotaldivider = playerInputController_Script.EmergencyPulseUseDelay;
        emergencyPulse_Slider.value = emergencyPulse_Slider.maxValue;
        empProgressBar_Animator.SetBool("Enemy_Goal_Hit", true);
    }

    private void GetReferences()
    {
        playerInputController_Script = FindObjectOfType<New_Input_System_Controller>();
        empProgressBar_Animator = GetComponent<Animator>();

        emergencyPulse_Slider = GetComponent<Slider>();
    }

    private void OnDisable()
    {
        playerInputController_Script.OnEmergencyPulseActivated -= UpdateRechargeBar;
    }

}
