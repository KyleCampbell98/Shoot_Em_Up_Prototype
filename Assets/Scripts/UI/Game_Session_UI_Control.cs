using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor.UIElements;

public class Game_Session_UI_Control : Menu_UI_Control
{
    //  Cached Scriptable Objects
    [SerializeField] private InPlay_Details currentGameSessionDetails;

    [Header("Text Mesh Refs")]
    [SerializeField] private TextMeshProUGUI survivalTimer_TMP;

    [Header("Canvas Refs")]
    [SerializeField] private GameObject in_Play_Panel;
    [SerializeField] private GameObject pause_Panel;
    [SerializeField] private GameObject game_Over_Panel;

    // Monobehaviour Methods
    private void Start()
    {
        if (FindObjectOfType<GameManager>() != null)
        {
            Debug.Log("GM found!");
            GameManager.m_GameStateChanged += DisplayCanvas;
        }
        else
        {
            Debug.Log("Game Manager missing!");
        }
        SetPanelActiveStatus(inplay: true);
    }

    // Update is called once per frame
    void Update()
    {
        survivalTimer_TMP.text = currentGameSessionDetails.CurrentGameSurvivalTime.ToString("00.00");
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
}
