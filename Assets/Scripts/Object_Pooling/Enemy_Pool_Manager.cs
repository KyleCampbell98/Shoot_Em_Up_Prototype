using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pool_Manager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    [SerializeField] private List<Enemy_Object_Pool> enemySpawners;
    [SerializeField] private Enemy_Object_Pool currentlyActivePool;

    // Spawning Parameters
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> SpawnPoints { get { return spawnPoints; } }
    private bool canSpawnEnemies;

    public bool CanSpawnEnemies { get { return canSpawnEnemies; } }

    // Events/Delegates
    //public delegate void OnSpawnPointsInitialized();
    public event Action SpawnPointsInitialized;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPoints();
        FindEnemySpawners();
    }

    private void FindEnemySpawners()
    {
        foreach(Transform t in transform)
        {  
            if(t.TryGetComponent(out Enemy_Object_Pool pool))
            {
                enemySpawners.Add(pool);
            }
        }
    }

    private void InitializeSpawnPoints()
    {
        var list = GetComponentsInChildren<Enemy_Spawn_Point_Ref>();
        foreach (var p in list)
        {
            spawnPoints.Add(p.gameObject.transform);
        }

        SpawnPointsInitialized?.Invoke();
        
    }

    
}
