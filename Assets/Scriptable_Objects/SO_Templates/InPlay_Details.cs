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

    [Header("Player Stats")]
    [SerializeField] private int playerHP;

    // Scoring properties
    public float CurrentGameSurvivalTime { get { return currentGameSurvivalTime; } set { currentGameSurvivalTime = value; } } //Constantly Ticks upwards from game start
    public int CurrentGameEnemiesDefeated { get { return currentGameEnemiesDefeated; } set { currentGameEnemiesDefeated = value; } } // Updated upon defeating an enemy

    public float BestSurvivalTime { get { return bestSurvivalTime; } private set {  bestSurvivalTime = value; } } // This property set by internal method only
    public int BestEnemiesDefeated { get { return bestEnemiesDefeated; } private set { bestEnemiesDefeated= value; } } // This property set by internal method only

    // Player stats properties 
    public int PlayerHP { get { return playerHP; } set { playerHP = value; } } // Needs to be assigned once hit

    // Internal Script Methods

    public bool CompareBestScores() // Called upon player defeat. deals with listing high scores at the main menu. 
    {
        bool newBestGameScore = false;

        if(CurrentGameSurvivalTime > BestSurvivalTime)
        {
            BestSurvivalTime = CurrentGameSurvivalTime;
            BestEnemiesDefeated = CurrentGameEnemiesDefeated;
            newBestGameScore = true;
            return newBestGameScore; // If returned true, popup will appear saying something like "New best!".
        }

        return newBestGameScore;
    }

    private void OnDisable()
    {
        CurrentGameSurvivalTime = 0f;
        CurrentGameEnemiesDefeated = 0;
    }

}
