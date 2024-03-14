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
    public static GameState CurrentGameState { get { return currentGameState; } private set { currentGameState = value; m_GameStateChanged?.Invoke(currentGameState); } } // null check here so that the delegate isnt called in the starting of a new session before other scripts have had chance to sub to this delegate. 
    // Theory that once changed, the Property will inform all subscribers through a triggered delegate that the state has changed.
    // All elements that need to change the state will call the static delegate with the new required state, then all elements that need to know the new state will be informed
    // through the property event call.

    [Header("Game Element Control")]
    [SerializeField] private int enemyDefeatsNeededForNextHPDrop;
    [SerializeField] private int currentEnemyDefeatProgress;
    [SerializeField] private bool canDropHPPickup;

    // Events and Delegates
    public delegate void GameStateChanged(GameState newState);
    public static GameStateChanged m_GameStateChanged;
    public static Action a_ActivatePause;
    public static Action a_GameOver;
    public static Action a_PlayerDefeatedEnemy; // Called every time an enemy is killed by a bomb
    public static Action a_PlayerValuesUpdated;
    public static Action a_ReleaseHPPickupDrop; // Called from within the script once the player has defeated enough enemies. 

    private void Start()
    {
        InternalEventSubscriptions();
        StartNewGame();
    }

    private void StartNewGame()
    {
        Debug.Log("Game Manager: New Game Started");
        
        if (currentGameState == GameState.Paused)
        {
            PauseGame(); // Unpauses game if it has loaded in a paused state.
        }
        currentGameSession.ResetGameSession();
    }

    private void InternalEventSubscriptions()
    {
        a_ActivatePause += PauseGame;
        a_GameOver += GameOver;
        a_PlayerDefeatedEnemy += PlayerDefeatedEnemy;
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
    private void PlayerDefeatedEnemy()
    {
        currentGameSession.CurrentGameEnemiesDefeated++;
        if(currentEnemyDefeatProgress >= enemyDefeatsNeededForNextHPDrop )
        {
            if (currentGameSession.PlayerHP < currentGameSession.StartingPlayerHP)
            {
                a_ReleaseHPPickupDrop?.Invoke();
            }
            else
            {
                // Add logic for increasing player score instead
            }

        }
        a_PlayerValuesUpdated?.Invoke();
    }

    private void OnDisable()
    {
        Debug.Log("On Disable called from GameManager");
        a_ActivatePause -= PauseGame;
        a_GameOver -= GameOver;
    }

}
