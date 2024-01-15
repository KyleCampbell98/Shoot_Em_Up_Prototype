using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave SO")]
public class Enemy_Wave_SO_Template : ScriptableObject
{
    [Header("Object References")]
    [SerializeField] private GameObject enemyToSpawn;
    
    [SerializeField] private Sprite setEnemySprite; // Used if "AllShapesRandom" is false. This means that the enemy has a set, defined shape.

    [Header("Enemy Attributes")]
    [Range(5, 30)][SerializeField] private float enemySpeed;


    [Header("Wave Type Parameters")]
    [SerializeField] private int enemyPoolSize;
    [SerializeField] private float spawnRate;
    [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType waveShapeType;

    [Header("Enemy Spawn Boolean Parameters")]
    [SerializeField] private bool spawnSequentially; // If true, a spawn rate will need to be set. If false, enemies will need to provide their own spawn logic.
    [SerializeField] private bool targetPlayer;
    [SerializeField] private bool allShapesRandom = false; // if set to yes, all shapes in the wave will be randomly assigned a shape.


    // Properties
    public GameObject EnemyToSpawn { get { return enemyToSpawn; } }
    public Sprite EnemySprite { get { return setEnemySprite; } }
    public Cutter_And_Enemy_Shape_Enums.ShapeType WaveShapeType { get { return waveShapeType; } }
    public int EnemyPoolSize { get { return enemyPoolSize; } }
    public float EnemySpeed { get { return enemySpeed; } }
    public float SpawnRate { get { if (SpawnSequentially) { return spawnRate; } else { Debug.Log("No spawn rate returned, enemy wave type doesn't support spawn rates"); return 0; } } } 
    public bool SpawnSequentially {  get { return spawnSequentially; } }
    public bool TargetPlayer { get {  return targetPlayer; } }
    public bool AllShapesRandom { get { return allShapesRandom; } }
    
}
