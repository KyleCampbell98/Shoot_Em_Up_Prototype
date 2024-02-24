using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSession_SO")]
public class InPlay_Details : ScriptableObject
{
    [Header("Scoring Attributes")]
    [SerializeField] private float currentGameSurvivalTime; // the survival time for the current, ongoing game session.
    [SerializeField] private int currentGameEnemiesDefeated; // enemies defeated in the current game session.
    [SerializeField] private float bestSurvivalTime;
    [SerializeField] private int bestEnemiesDefeated; // This will always be the enemies defeated in the game with the best survival time, not the best overall enemies defeated regardless of survival

    [Header("Player Information")]
    [SerializeField] private int playerHP;
    [SerializeField] private int bombsRemaining;

    [Header("Game Session Defaults")]
    [SerializeField] private int startingPlayerHP;
    [SerializeField] private int startingPlayerBombs;
    [SerializeField] private int bombAmmoCap = 6; // Can be used later for increasing max ammo as an upgrade;
    // Scoring properties
    public float CurrentGameSurvivalTime { get { return currentGameSurvivalTime; } set { currentGameSurvivalTime = value; } } //Constantly Ticks upwards from game start
    public int CurrentGameEnemiesDefeated { get { return currentGameEnemiesDefeated; } set { currentGameEnemiesDefeated = value; } } // Updated upon defeating an enemy

    public float BestSurvivalTime { get { return bestSurvivalTime; } private set {  bestSurvivalTime = value; } } // This property set by internal method only
    public int BestEnemiesDefeated { get { return bestEnemiesDefeated; } private set { bestEnemiesDefeated= value; } } // This property set by internal method only

    // Player Info properties
    public int PlayerHP { get { return playerHP; } set {  playerHP = value; } }
    public int BombsRemaining { get {  return bombsRemaining; } set { if (value <= 0) { bombsRemaining = 0; } else { bombsRemaining = value; } } }

    // Internal Script Methods

    public bool CompareBestScores() // Called upon player defeat. deals with listing high scores at the main menu. 
    {
        bool newBestGameScore = false; // Bool used for UI control purposes

        if(CurrentGameSurvivalTime > BestSurvivalTime)
        {
            BestSurvivalTime = CurrentGameSurvivalTime;
            BestEnemiesDefeated = CurrentGameEnemiesDefeated;
            newBestGameScore = true;
            OnDisable();
            return newBestGameScore; // If returned true, popup will appear saying something like "New best!".
        }
        OnDisable();

        return newBestGameScore;
    }

    public void ResetHighScores()
    {
        BestEnemiesDefeated = 0;
        BestSurvivalTime = 0f;
    }

    private void OnDisable()
    {
        CurrentGameSurvivalTime = 0f;
        CurrentGameEnemiesDefeated = 0;
        PlayerHP = startingPlayerHP;
        BombsRemaining = startingPlayerBombs;
    }

}
