using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave SO")]
public class Enemy_Wave_SO_Template : ScriptableObject
{
    /// <summary>
    ///  This template controls aspects of the enemies within a wave. These are essentially the external modifiers to an enemy's defined behaviour
    ///  For example, this template defines the speed of enemy movement, not the actual movement behaviour itself. 
    ///  The wave will also define an identity for each enemy within it through the list of provided "Potential Shapes" (Shape Info SOs).
    /// </summary>

    [Header("Object References")]
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private Shape_Info[] potentialShapes;

    [Header("Enemy Attributes")]
    [Range(5, 30)][SerializeField] private float enemySpeed;

    [Header("Wave Type Parameters")]
    [SerializeField] private int enemyPoolSize;
    [SerializeField] private float spawnRate;

    [Header("Enemy Spawn Boolean Parameters")]
    [SerializeField] private bool spawnSequentially; // If true, a spawn rate will need to be set. If false, enemies will need to provide their own spawn logic.
    [SerializeField] private bool targetPlayer;
    [SerializeField] private bool allShapesRandom = false; // if set to yes, all shapes in the wave will be randomly assigned a shape.


    // Properties
    public GameObject EnemyToSpawn { get { return enemyToSpawn; } }
    public Shape_Info EnemyShape { get { if (!allShapesRandom) { return potentialShapes[0]; } else  return potentialShapes[UnityEngine.Random.Range(0, potentialShapes.Length)]; } } 
    public int EnemyPoolSize { get { return enemyPoolSize; } }
    public float EnemySpeed { get { return enemySpeed; } }
    public float SpawnRate { get { if (SpawnSequentially) { return spawnRate; } else { Debug.LogError("No spawn rate returned, enemy wave type doesn't support spawn rates"); return 0; } } } 
    public bool SpawnSequentially {  get { return spawnSequentially; } }
    public bool TargetPlayer { get {  return targetPlayer; } }
    public bool AllShapesRandom { get { return allShapesRandom; } }

    

}
