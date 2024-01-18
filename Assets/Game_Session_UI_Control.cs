using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Session_UI_Control : MonoBehaviour
{
    [SerializeField] private InPlay_Details currentGameSessionDetails;

    [SerializeField] private TextMeshProUGUI survivalTimer_TMP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        survivalTimer_TMP.text = currentGameSessionDetails.SurvivalTime.ToString("00.00.00");
    }
}
