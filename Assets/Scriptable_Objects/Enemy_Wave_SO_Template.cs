using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave SO")]
public class Enemy_Wave_SO_Template : ScriptableObject
{
    [Header("Object References")]
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType waveShapeType;
    [SerializeField] private Sprite enemySprite;

    [Header("Wave Type Parameters")]
    [SerializeField] private int enemyPoolSize;
    [SerializeField] private float spawnRate;

    [Header("Enemy Spawn Boolean Parameters")]
    [SerializeField] private bool spawnSequentially; // If true, a spawn rate will need to be set. If false, enemies will need to provide their own spawn logic.
    [SerializeField] private bool targetPlayer;


    // Properties
    public GameObject EnemyToSpawn { get { return enemyToSpawn; } }
    public Sprite EnemySprite { get { return enemySprite; } }
    public int EnemyPoolSize { get { return enemyPoolSize; } }
    public float SpawnRate { get { if (SpawnSequentially) { return spawnRate; } else { Debug.Log("No spawn rate returned, enemy wave type doesn't support spawn rates"); return 0; } } } 
    public bool SpawnSequentially {  get { return spawnSequentially; } }
    public bool TargetPlayer { get {  return targetPlayer; } }
    
}
