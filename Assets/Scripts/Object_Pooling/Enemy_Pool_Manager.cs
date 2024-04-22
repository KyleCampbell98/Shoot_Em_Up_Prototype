using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pool_Manager : MonoBehaviour
{
    [SerializeField] int enemySpawnerIndexer = 0;

    [Header("Enemy Spawners")]
    [SerializeField] private List<Enemy_Object_Pool> enemySpawners;
    [SerializeField] private Enemy_Object_Pool currentlyActivePool;
    
    [Header("Spawning Parameters")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [Header("Difficulty Scaling Parameters")]
    [SerializeField] private float movementMultiplier = 1.5f;

    // Properties
    private Enemy_Object_Pool CurrentlyActivePool 
    { 
        get { return currentlyActivePool; } 
        set { currentlyActivePool = value; currentlyActivePool.SpawnTimerActive = true; } 
    }
    public List<Transform> SpawnPoints { get { return spawnPoints; } }
   
    // Events Management
    public event Action SpawnPointsInitialized; // Called to tell Pools to populate their object pools
    public Action EnemyPoolLifespanOver; // Each object pool will call this when their lifespan ends

    // Monobehaviour Methods
    void Start()
    {
        InitializeSpawnPoints();
        FindEnemySpawners();
        CurrentlyActivePool = enemySpawners[0];
        EnemyPoolLifespanOver += ShutdownCurrentPool;
    }

    // Spawner Management Methods
    private void ShutdownCurrentPool() // This method will deactivate the current pool, eit its spawn parameters for difficulty, and activate the next pool.
    {
        Debug.Log($"{CurrentlyActivePool.gameObject.name} pool has been shut down.");
        CurrentlyActivePool.SpawnTimerActive = false;
        ApplyDifficultyScaling(CurrentlyActivePool);
        CurrentlyActivePool = SetNewCurrentActivePool();
        CurrentlyActivePool.SpawnTimerActive = true;
    }

    // Spawner Editing Methods
    private void ApplyDifficultyScaling(Enemy_Object_Pool enemy_Object_Pool)
    {
        
        foreach(GameObject enemy in CurrentlyActivePool.EnemyPool)
        {
            Debug.Log("Apply difficulty settings SUCCESS");
            enemy.GetComponentInChildren<Enemy>().SetUpEnemy(movementMultiplier);
        }
      //  enemy_Object_Pool.enemyWaveData.EnemySpeed *= 1.25f;
    }

    // Initialization Methods

    private void InitializeSpawnPoints()
    {
        var list = GetComponentsInChildren<Enemy_Spawn_Point_Ref>();
        foreach (var p in list)
        {
            spawnPoints.Add(p.gameObject.transform);
        }

        SpawnPointsInitialized?.Invoke();

    }
    private void FindEnemySpawners()
    {
        foreach (Transform t in transform)
        {
            if (t.TryGetComponent(out Enemy_Object_Pool pool))
            {
                enemySpawners.Add(pool);
            }
        }
    }

    // Internal Script Logic
    private void OnDisable()
    {
        EnemyPoolLifespanOver -= ShutdownCurrentPool;
    }

    private Enemy_Object_Pool SetNewCurrentActivePool()
    {
       
        if(enemySpawnerIndexer + 1 >= enemySpawners.Count)
        {
            enemySpawnerIndexer = 0;
            CurrentlyActivePool = enemySpawners[enemySpawnerIndexer];
        }
        else
        {
            enemySpawnerIndexer++;
            CurrentlyActivePool = enemySpawners[enemySpawnerIndexer];
        }

        return CurrentlyActivePool;
    }
}
