using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor.UIElements;
using System.Runtime.InteropServices;
using UnityEngine.Events;

public class Game_Session_UI_Control : Menu_UI_Control
{
    //  Cached Scriptable Objects
    [SerializeField] private InPlay_Details currentGameSessionDetails;

    [Header("Text Mesh Refs")]
    [SerializeField] private TextMeshProUGUI survivalTimer_TMP;
    [SerializeField] private TextMeshProUGUI bombCounter_TMP;
    [SerializeField] private TextMeshProUGUI playerHP_TMP;
    [SerializeField] private TextMeshProUGUI newBestSurvivalTimeDisplay;
    

    [Header("Canvas Refs")]
    [SerializeField] private GameObject in_Play_Panel;
    [SerializeField] private GameObject pause_Panel;
    [SerializeField] private GameObject game_Over_Panel;
    [SerializeField] private GameObject newHighScoreDisplay;

    
    // Monobehaviour Methods
    private void Start()
    {
        UpdateOnScreenUI(); 
        if (FindObjectOfType<GameManager>() != null)
        {
            Debug.Log("GM found!");
            GameManager.m_GameStateChanged += DisplayCanvas;
            GameManager.a_GameOver += ResetScoringInfo;
            GameManager.a_playerValuesUpdated += UpdateOnScreenUI;
        }
        else
        {
            Debug.LogError("Game Manager missing!");
        }
        SetPanelActiveStatus(inplay: true);
    }



    // Update is called once per frame
    void Update()
    {
        survivalTimer_TMP.text = currentGameSessionDetails.CurrentGameSurvivalTime.ToString("00.00");
    }

    private void ResetScoringInfo()
    {
        bool newBestSet = currentGameSessionDetails.CompareBestScores();
        newBestSurvivalTimeDisplay.text = currentGameSessionDetails.BestSurvivalTime.ToString("00:00");
        newHighScoreDisplay.SetActive(newBestSet);
        
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


    }

    private void OnDisable()
    {
        GameManager.m_GameStateChanged -= DisplayCanvas;
        GameManager.a_GameOver -= ResetScoringInfo;
        GameManager.a_playerValuesUpdated -= UpdateOnScreenUI;
    }
}
