using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UI_Scoring_Control : Menu_UI_Control
{
    [Header("Scoring Cached Storage")]
    [SerializeField] private InPlay_Details scoringCache;

    [Header("UI Element References")]
    [SerializeField] private TextMeshProUGUI bestTimeSurvived_TMP;
    [SerializeField] private TextMeshProUGUI bestEnemiesDefeated_TMP;

    private void Start()
    {
        Debug.Log("Scene Load call test");
        bestEnemiesDefeated_TMP.text = scoringCache.BestEnemiesDefeated.ToString();
        bestTimeSurvived_TMP.text = FormatRawTime(scoringCache.BestSurvivalTime);
    }

    public void ResetGameScores()
    {
        scoringCache.ResetHighScores();
        bestEnemiesDefeated_TMP.text = scoringCache.BestEnemiesDefeated.ToString();
        bestTimeSurvived_TMP.text = FormatRawTime(scoringCache.BestSurvivalTime);
    }


}
