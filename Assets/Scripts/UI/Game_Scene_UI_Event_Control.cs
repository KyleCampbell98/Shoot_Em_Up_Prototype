using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Game_Scene_UI_Event_Control : UI_Event_Control
{
    // Button Panel Defaults
    [SerializeField] private GameObject continueGameGameOverButton;
    [SerializeField] private GameObject confirmQuitToMenuButton;
    [SerializeField] private GameObject confirmQuitToDesktopButton;
    [SerializeField] private GameObject gameOverDefaultButton;
    [SerializeField] private GameObject returnFromControlsScreenButton;

    // Start is called before the first frame update
    new void Start()
    {
        if (FindObjectOfType<GameManager>())
        {
            GameManager.m_GameStateChanged += OnPauseUIShown;
        }
        else
        {
            Debug.LogError("Game Scene UI Event Control: No Game Manager Found! Could not assign event listeners.");
        }
    }

    private void OnPauseUIShown(GameManager.GameState currentState)
    {
        if(currentState == GameManager.GameState.Paused)
        {
            ResetCurrentSelectedObject(initialButtonSelection); // initial Button selection in Game Scene will always be Pause Button.

        }
        else if (currentState == GameManager.GameState.In_Play)
        {
            ResetCurrentSelectedObject(null);
        }
        else if(currentState == GameManager.GameState.Game_Over)
        {
            ResetCurrentSelectedObject(gameOverDefaultButton);
        }
    }

   

    public void ReturnToPauseScreen()
    {
        ResetCurrentSelectedObject(initialButtonSelection);
    }

    public void OnQuitButtonSelection(bool quittingToDesktop)
    {
        if (!quittingToDesktop)
        {
            ResetCurrentSelectedObject(confirmQuitToMenuButton);
        }
        else
        {
            ResetCurrentSelectedObject(confirmQuitToDesktopButton);
        }
    }

    public void OnControlsScreenShow()
    {
        ResetCurrentSelectedObject(returnFromControlsScreenButton);
    }
    private void OnDisable()
    {
        GameManager.m_GameStateChanged -= OnPauseUIShown;
    }

}
