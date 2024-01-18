using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSession_SO")]
public class InPlay_Details : ScriptableObject
{
    [Header("Scoring Attributes")]
    [SerializeField] private float survivalTime;
    [SerializeField] private int enemiesDefeated;

    [Header("Player Stats")]
    [SerializeField] private int playerHP;

    // Scoring properties
    [SerializeField] public float SurvivalTime { get { return survivalTime; } set { survivalTime = value; } } //Constantly Ticks upwards from game start
    [SerializeField] public int EnemiesDefeated { get { return enemiesDefeated; } set { enemiesDefeated = value; } } // Updated upon defeating an enemy


    // Player stats properties 
    [SerializeField] public int PlayerHP { get { return playerHP; } set { playerHP = value; } } // Needs to be assigned once hit

    private void OnDisable()
    {
        SurvivalTime = 0f;
    }

}
