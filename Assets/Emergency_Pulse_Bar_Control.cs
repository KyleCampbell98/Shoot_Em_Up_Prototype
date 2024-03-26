using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Emergency_Pulse_Bar_Control : MonoBehaviour
{
    [SerializeField] private Slider emergency_Pulse_Slider;
    private float bar_Total_divider; // The number that the overall bar total will be divided by. EG: Bar takes 20 seconds to reacharge, divider = 20;

    private bool updateBar; // if false, the update operation lerp wont run. 

    // Start is called before the first frame update
    void Start()
    {
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
        StartCoroutine(RechargeBar());
    }

    private IEnumerator RechargeBar()
    {
        yield return new WaitUntil(() => emergency_Pulse_Slider.value == emergency_Pulse_Slider.maxValue);
    }

}
