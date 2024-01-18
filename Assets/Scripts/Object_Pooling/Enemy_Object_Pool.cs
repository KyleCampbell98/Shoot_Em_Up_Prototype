using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Enemy_Object_Pool : Object_Pool_Template
{
    [Header("Cached References")]
    [SerializeField] private Enemy_Wave_SO_Template enemyWaveData;
    [SerializeField] private GameObject waveTarget;
    private Collider2D waveAreaTarget; // For use in setting a reference to the general play area.

    [Header("Enemy Activation")]
    [SerializeField] private float spawnTimer;
    private float SpawnTimer { get { return spawnTimer; } set { spawnTimer = value; if (value <= 0) { SpawnTimerHitZero(); } } }
    [SerializeField] private List<Transform> spawnPoints;

    // Internal Script Control 
    private bool canPopulatePool = false;
    private bool spawnTimerActive = false;

    private event UnityAction SpawnTimerHitZero;

    // Unity Methods

    private void Awake()
    {
        InitializePoolSpawnPoints(); // is called once before the pool is populated. MUST BE CALLED BEFORE POOL POPULATION, as if called, after, the enemies will all be added as spawn points too.
    }

    void Start()
    {
        EventSubscriptions();
        InitialPoolSetup();
        if (canPopulatePool)
        {
            PopulatePool();
            spawnTimerActive = true;
        }
        else
        {
            Debug.LogError("PopulatePool() in Enemy_Object_Pool not called as the pool's spawn points weren't initially set first. \n" +
                "InitializePoolSpawnPoints() must be called before PopulatePool()");
        }
    }

    private void Update()
    {
        SpawnCountdownTimer();
    }

    // Spawning Methods

    private void SpawnCountdownTimer() // used for initial testing, can be refined later
    {
        if (spawnTimerActive == true)
        {
            SpawnTimer -= Time.deltaTime;
        }
    }

    private void ActivateEnemyOnSpawnRate()
    {
        GameObject enemyToActivate = GetPooledObject();
        enemyToActivate.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
        enemyToActivate.SetActive(true);
        enemyToActivate = null;
        spawnTimer = enemyWaveData.SpawnRate;
    }

    // Pool Initialization Methods

    private void InitialPoolSetup()
    {
        objectToPool = enemyWaveData.EnemyToSpawn;
        objectPoolSize = enemyWaveData.EnemyPoolSize;
        spawnTimer = enemyWaveData.SpawnRate;
        SetObjectParentToSelf();
        SetTarget();
    }

    protected override void PopulatePool()
    {
        Debug.Log("Populate Object pool called");

        pooledObjects = new GameObject[objectPoolSize];
        for (int i = 0; i < objectPoolSize; i++)
        {
            pooledObjects[i] = Instantiate(objectToPool, transform.position, Quaternion.identity, pooledObjectParent);

            Enemy localEnemyScript; // local Cached enemy script for Enemy setup;
            if (pooledObjects[i].GetComponent<Enemy>() == null) 
            { 
                localEnemyScript = pooledObjects[i].GetComponentInChildren<Enemy>(); 
            }
            else 
            { 
                localEnemyScript = pooledObjects[i].GetComponent<Enemy>(); 
            }

            localEnemyScript.SetUpEnemy(enemyWaveData.EnemySpeed, enemyWaveData.EnemyShape, waveTarget); // Changed this m,ethod to only call for the shape info once, as calliong for sprite and type 
            // separately was causing 2 random.range calculations, leading to mismatched data in each property call.  
            
            pooledObjects[i].SetActive(false);
        }
    }

    private void InitializePoolSpawnPoints()
    {
        Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).transform);
        }

        canPopulatePool = true;
    }

    // Internal Script Logic Methods

    private void EventSubscriptions()
    {
        SpawnTimerHitZero += ActivateEnemyOnSpawnRate;
    }

    private void SetTarget()
    {
        switch (enemyWaveData.TargetPlayer)
        {
            case true:
                waveTarget = PlayerRefManager.PlayerReference;
                break;

            case false:
                waveTarget = PlayAreaRefManager.PlayAreaBounds;
                break;

            default:

        }
    }

    private void SetSpawnPosition() 
    {

    }

    private IEnumerator SequentialEnemySpawner()
    {

       GetNextObject(arrayControl).SetActive(true);

        yield return new WaitForSeconds(enemyWaveData.SpawnRate);
        StartCoroutine(SequentialEnemySpawner());
        Debug.Log("Reached here during coroutine.");
    } // Initial enemy spawning test.

  
}
