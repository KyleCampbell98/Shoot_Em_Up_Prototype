using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Emergency_Pulse_Bar_Control : MonoBehaviour
{

    [SerializeField] private New_Input_System_Controller player_Input_Controller_Script;
    [SerializeField] private Slider emergency_Pulse_Slider;

    private float bar_Total_divider; // The number that the overall bar total will be divided by. EG: Bar takes 20 seconds to reacharge, divider = 20;

    [SerializeField] private Slider emergency_Pulse_Slider;
    private float bar_Total_divider; // The number that the overall bar total will be divided by. EG: Bar takes 20 seconds to reacharge, divider = 20;


    private bool updateBar; // if false, the update operation lerp wont run. 

    // Start is called before the first frame update
    void Start()
    {

        GetReferences();
        player_Input_Controller_Script.OnEmergencyPulseActivated += UpdateRechargeBar;
        bar_Total_divider = player_Input_Controller_Script.EmergencyPulseUseDelay;
    }

    private void GetReferences()
    {
        player_Input_Controller_Script = FindObjectOfType<New_Input_System_Controller>();


        emergency_Pulse_Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateBar)
        {
          //  Mathf.Lerp(emergency_Pulse_Slider.minValue, emergency_Pulse_Slider.maxValue, )
        }
    }

    private void UpdateRechargeBar()
    {

        emergency_Pulse_Slider.value = 0;


        StartCoroutine(RechargeBar());
    }

    private IEnumerator RechargeBar()
    {

        float timeElasped = 0f;

        while(timeElasped < bar_Total_divider)
        {
            Debug.Log("Emergency_Pulse_Slider_Control: Recharge Bar Coroutine Activated");
            emergency_Pulse_Slider.value = Mathf.Lerp(emergency_Pulse_Slider.minValue, emergency_Pulse_Slider.maxValue, timeElasped / bar_Total_divider);
            timeElasped += Time.deltaTime;
            yield return null;
        }
      
    }

    private void OnDisable()
    {
        player_Input_Controller_Script.OnEmergencyPulseActivated -= UpdateRechargeBar;


        yield return new WaitUntil(() => emergency_Pulse_Slider.value == emergency_Pulse_Slider.maxValue);

    }

}
