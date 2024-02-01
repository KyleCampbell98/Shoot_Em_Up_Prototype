using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;

    [SerializeField] private float timerDelay = 3;
    bool startSurvivalTimer = false;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySurvivalTimeStart(timerDelay));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSurvivalTimer) { return; }
        currentGameSession.CurrentGameSurvivalTime += Time.deltaTime;
    }

    private IEnumerator DelaySurvivalTimeStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        startSurvivalTimer = true;

    }
}
