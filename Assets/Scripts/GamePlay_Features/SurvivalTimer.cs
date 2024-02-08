using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;
    private bool pauseTimer;

    [SerializeField] private float timerDelay = 3;
    bool startSurvivalTimer = false;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySurvivalTimeStart(timerDelay));
        GameManager.m_GameStateChanged += DisableSurvivalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startSurvivalTimer) { return; }
        if (pauseTimer) { return; }
        currentGameSession.CurrentGameSurvivalTime += Time.deltaTime;
    }

    private void DisableSurvivalTimer(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.In_Play:
                pauseTimer = false;
                break;
          
            default:
                pauseTimer = true;
                break;
        }
    }

    private IEnumerator DelaySurvivalTimeStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        startSurvivalTimer = true;

    }

    private void OnDisable()
    {
        GameManager.m_GameStateChanged -= DisableSurvivalTimer;
    }
}
