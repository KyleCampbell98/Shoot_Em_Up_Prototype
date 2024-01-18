using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlay_Details : ScriptableObject
{
    // Scoring properties
    [SerializeField] private float SurvivalTime {  get; set; }
    [SerializeField] private int EnemiesDefeated {  get; set; }
    

    // Player Stats
    [SerializeField] private int PlayerHP {  get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
