using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;

    // State Management
    public enum GameState { In_Play, Paused, At_Menu, Game_Over }

    private static GameState currentGameState;
    public static GameState CurrentGameState { get { return currentGameState; } private set { currentGameState = value; m_GameStateChanged(currentGameState); } }
    // Theory that once changed, the Property will inform all subscribers through a triggered delegate that the state has changed.
    // All elements that need to change the state will call the static delegate with the new required state, then all elements that need to know the new state will be informed
    // through the property event call.

    // Events and Delegates
    public delegate void GameStateChanged(GameState newState);
    public static GameStateChanged m_GameStateChanged;
    public static Action a_ActivatePause;
    public static Action a_GameOver;

    public static Action a_playerValuesUpdated;


    private void Start()
    {
        InternalEventSubscriptions();
    }

    private void InternalEventSubscriptions()
    {
        a_ActivatePause += PauseGame;
        a_GameOver += GameOver;
    }

    private void GameOver()
    {
        CurrentGameState = GameState.Game_Over;
        
    }
    private void PauseGame()
    {
        switch (CurrentGameState)
        {
            case GameState.In_Play:
                CurrentGameState = GameState.Paused;
                Debug.Log("Game should now be paused");
                Time.timeScale = 0;

                break;

            case GameState.Paused:
                CurrentGameState = GameState.In_Play;
                Time.timeScale = 1;

                break;

            default:
                Debug.LogError("Dead or At menu. Cannot Pause Right Now."); // Handles other states such as being at the menu or already dead. 
                break;
        }
    }

    private void OnDisable()
    {
        a_ActivatePause -= PauseGame;
        a_GameOver -= GameOver;
    }

}
