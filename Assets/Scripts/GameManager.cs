using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { In_Play, Paused, At_Menu, Game_Over} // Potentially needs to be expanded on. Only implemented for now as a reminder that some form of state management is needed. 

    private static GameState currentGameState;
    public static GameState CurrentGameState { get { return currentGameState; }  private set { currentGameState = value; m_GameStateChanged(currentGameState); } } 
    // Theory that once changed, the Property will inform all subscribers through a truiggered delegate that the state has changed.
    // All elements that need to change the state will call the static delegate with the new required state, then all elements that need to know the new state will be informed
    // through the property event call.

    // Events and Delegates
    public delegate void  GameStateChanged(GameState newState);
    public static GameStateChanged m_GameStateChanged;
    public static Action ActivatePause;

    [Header("Cached References")]
    [SerializeField] private InPlay_Details currentGameSession;

    private void Start()
    {
        EventSubscriptions();
       
        
    }

    private void EventSubscriptions()
    {
        ActivatePause += PauseGame;
    }

    private void PauseGame()
    {
      
    }
}
