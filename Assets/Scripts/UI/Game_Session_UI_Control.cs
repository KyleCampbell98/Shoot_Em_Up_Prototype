using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game_Session_UI_Control : Menu_UI_Control
{
    //  Cached Scriptable Objects
    [SerializeField] private InPlay_Details currentGameSessionDetails;

    [Header("Text Mesh Refs")]
    [SerializeField] private TextMeshProUGUI survivalTimer_TMP;
    [SerializeField] private TextMeshProUGUI bombCounter_TMP;
    [SerializeField] private TextMeshProUGUI playerHP_TMP;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI newBestSurvivalTimeDisplay;
    
    [Header("Canvas Refs")]
    [SerializeField] private GameObject in_Play_Panel;
    [SerializeField] private GameObject pause_Panel;
    [SerializeField] private GameObject game_Over_Panel;
    [SerializeField] private GameObject newHighScoreDisplay;
    [SerializeField] private GameObject resultsDisplay;
    [SerializeField] private GameObject speedUpMessage;
    [SerializeField] private GameObject survivalBonusMessage;

    private string completeFormattedTime;

    // Monobehaviour Methods
    private void Start()
    {
        UpdateOnScreenUI(); 
        if (FindObjectOfType<GameManager>() != null)
        {
            Debug.Log("Game Session UI Controller: Game Manager found");
            GameManager.m_GameStateChanged += DisplayCanvas;
            GameManager.a_GameOver += ResetScoringInfo;
            GameManager.a_PlayerValuesUpdated += UpdateOnScreenUI;
            GameManager.a_spawnerRoundComplete += SpeedUpMessageEnabler;
            GameManager.a_bonusTimeAdded += BonusMessageEnabler;
        }
        else
        {
            Debug.LogError("Game Session UI Controller: Game Manager missing!");
        }
        SetPanelActiveStatus(inplay: true);
    }



    // Update is called once per frame
    void Update()
    {
        completeFormattedTime = FormatRawTime(currentGameSessionDetails.CurrentGameSurvivalTime);
        survivalTimer_TMP.text = completeFormattedTime;
    }

   

    private void ResetScoringInfo()
    {
        string sessionEndTime = FormatRawTime(currentGameSessionDetails.CurrentGameSurvivalTime);
        bool newBestSet = currentGameSessionDetails.CompareBestScores();

        newBestSurvivalTimeDisplay.text = sessionEndTime;
        resultsText.text = sessionEndTime;

        if (newBestSet)
        {
            resultsDisplay.SetActive(false);
            newHighScoreDisplay.SetActive(true);
        }
        else if(!newBestSet)
        {
            resultsDisplay.SetActive(true);
            newHighScoreDisplay.SetActive(false);

        }
    }

    private void DisplayCanvas(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.In_Play:
                SetPanelActiveStatus(inplay: true);

                break;
            case GameManager.GameState.Paused:
                SetPanelActiveStatus(pause: true);
                break;
            
            case GameManager.GameState.Game_Over:
                SetPanelActiveStatus(gameOver: true);
                break;
            default:
                break;
        }
    }

    private void SpeedUpMessageEnabler()
    {
        Debug.Log("Speed Up Message Activated");
        speedUpMessage.gameObject.SetActive(true);
    }
    private void BonusMessageEnabler()
    {
        Debug.Log("Bonus Message Activated");
        survivalBonusMessage.gameObject.SetActive(true);
    }

    private void SetPanelActiveStatus(bool inplay = false, bool pause = false, bool gameOver = false)
    {
        in_Play_Panel.SetActive(inplay);
        pause_Panel.SetActive(pause);
        game_Over_Panel.SetActive(gameOver);
    }

    private void UpdateOnScreenUI()
    {
        bombCounter_TMP.text = "Bombs: " + currentGameSessionDetails.BombsRemaining.ToString();
        playerHP_TMP.text = "Health: " + (currentGameSessionDetails.PlayerHP + 1).ToString();
        survivalTimer_TMP.text = FormatRawTime(currentGameSessionDetails.CurrentGameSurvivalTime);
        


    }

    private void OnDisable()
    {
        GameManager.m_GameStateChanged -= DisplayCanvas;
        GameManager.a_GameOver -= ResetScoringInfo;
        GameManager.a_PlayerValuesUpdated -= UpdateOnScreenUI;
        GameManager.a_spawnerRoundComplete -= SpeedUpMessageEnabler;
        GameManager.a_bonusTimeAdded -= BonusMessageEnabler;
    }
}
