using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;

    [SerializeField] private float timerDelay = 3;

    private float timeSurvived;
    bool startSurvivalTimer = false;

    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySurvivalTimeStart(timerDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSurvivalTimer) { return; }
        currentGameSession.SurvivalTime += Time.deltaTime;
    }

    private IEnumerator DelaySurvivalTimeStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        startSurvivalTimer = true;

    }
}
