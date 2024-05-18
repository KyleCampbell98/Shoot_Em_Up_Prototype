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

    // Game Element Control
    private static int enemyDefeatsNeededForNextHPDrop = 4;
    [SerializeField] private static int currentEnemyDefeatProgress;
    [SerializeField] private int displayCurrentEnemyDefeatProgress; // DELETE BEFORE RELEASE. ONLY HERE TO MONITOR A STATIC VARIABLE.
    private static int amountToIncreaseEnemyDefeatRequirementBy = 1; // Every time a health pickup is dropped, the next pickup is made more difficult to obtain due to this value. 

    public static int EnemyDefeatsNeededForNextHPDrop { get { return enemyDefeatsNeededForNextHPDrop; } }
  //  public static int AmountToIncreaseEnemyDefeatRequirementBy { get { return amountToIncreaseEnemyDefeatRequirementBy; } } Really this shouldnt be needed by the slider as it should be updated as a value overall, and then that value used to set the slider max value.

    // Events and Delegates
    public delegate void GameStateChanged(GameState newState);
    public static GameStateChanged m_GameStateChanged;
    public static Action a_ActivatePause;
    public static Action a_GameOver;
    public static Action a_PlayerDefeatedEnemy; // Called every time an enemy is killed by a bomb
    public static Action a_PlayerValuesUpdated;
    public static Action a_ReleaseHPPickupDrop; // Called from within the script once the player has defeated enough enemies. 
    public static Action<bool> a_PlayerCollectedPickup;
    public static Action a_spawnerRoundComplete;
    public static Action a_bonusTimeAdded;

    public static bool health_Pickup_Is_Active = false;

    private void Start()
    {
        InternalEventSubscriptions();
        StartNewGame();
    }

    private void Update()
    {
        displayCurrentEnemyDefeatProgress = currentEnemyDefeatProgress;
    }

    private void StartNewGame()
    {   
        if (currentGameState == GameState.Paused)
        {
            PauseGame(); // Unpauses game if it has loaded in a paused state.
        }
        else
        {
            currentGameState = GameState.In_Play;
        }
        currentGameSession.ResetGameSession();
    }

    private void InternalEventSubscriptions()
    {
        a_ActivatePause += PauseGame;
        a_GameOver += GameOver;
        a_PlayerDefeatedEnemy += PlayerDefeatedEnemy;
        a_PlayerCollectedPickup += EditPlayerStatus;
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
                Debug.Log("Game Manager: Game should now be paused");
                Time.timeScale = 0;

                break;

            case GameState.Paused:
                CurrentGameState = GameState.In_Play;
                Debug.Log("Game Manager: Game should now NOT be paused");
                Time.timeScale = 1;

                break;

            default:
                Debug.LogError("Game Manager: Dead or At menu. Cannot Pause Right Now."); // Handles other states such as being at the menu or already dead. 
                break;
        }
    }
    private void PlayerDefeatedEnemy()
    {
       
        currentGameSession.CurrentGameEnemiesDefeated++;
        currentEnemyDefeatProgress++;
        Debug.Log("Enemies Defeated: " + currentEnemyDefeatProgress + "\nEnemies needed for drop: " + enemyDefeatsNeededForNextHPDrop);
        if (currentEnemyDefeatProgress >= enemyDefeatsNeededForNextHPDrop )
        {
            if (currentGameSession.PlayerHP < currentGameSession.StartingPlayerHP)
            {
                if (health_Pickup_Is_Active) { Debug.Log("Game Manager: Health Pickup already on field. No Pickup Activated"); return; }
                Debug.Log("Game Manager: Dropping HP pickup.");
                enemyDefeatsNeededForNextHPDrop += amountToIncreaseEnemyDefeatRequirementBy; // Requirements only increase when player has a health drop spawned
                a_ReleaseHPPickupDrop?.Invoke();
                health_Pickup_Is_Active = true;
            }
            else
            {
                Pickup_Slider_Controller.a_ResetSlider();
                currentGameSession.CurrentGameSurvivalTime += 5f;
                a_bonusTimeAdded?.Invoke();
                Debug.Log("Game Manager: Adding bonus points to player score.");

            }
          
            currentEnemyDefeatProgress = 0;
        }
        a_PlayerValuesUpdated?.Invoke();
    }
    private void EditPlayerStatus(bool giveHealth)
    {
        switch (giveHealth)
        {
            case false:
                Debug.Log("Game Manager: Increasing Player Bombs");
                currentGameSession.BombsRemaining++;
                a_PlayerValuesUpdated?.Invoke();
                break;

                case true:
                Debug.Log("Game Manager: Increasing Player Health");
                currentGameSession.PlayerHP = currentGameSession.StartingPlayerHP;
                
                a_PlayerValuesUpdated?.Invoke();
                break;
            default:
            Debug.LogError("Game Manager: Triggered a player pickup collision, but no pickup was associated with the event trigger. ");
                break;
        }
    }

    private void OnDisable()
    {
        currentEnemyDefeatProgress = 0;
        a_ActivatePause -= PauseGame;
        a_GameOver -= GameOver;
        a_PlayerDefeatedEnemy -= PlayerDefeatedEnemy; // Added 18/05
        a_PlayerCollectedPickup -= EditPlayerStatus; // Added 18/05
    }

}
