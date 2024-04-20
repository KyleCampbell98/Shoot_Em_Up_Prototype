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
    [SerializeField] public Enemy_Wave_SO_Template enemyWaveData;
    [SerializeField] private GameObject waveTarget;
    [SerializeField] private Enemy_Pool_Manager enemyPoolManagerScript;
   // private Collider2D waveAreaTarget; // For use in setting a reference to the general play area.

    [Header("Enemy Activation")]
    [SerializeField] private float spawnTimer;
    [SerializeField] private float poolLifeSpan; // how long the pool will remain active for until the next pool is activated;
    private float SpawnTimer { get { return spawnTimer; } set { spawnTimer = value; if (value <= 0) { SpawnTimerHitZero(); } } }

    // Internal Script Control 
    private bool spawnTimerActive = false;

    private event UnityAction SpawnTimerHitZero;

    // Unity Methods

    private void Awake()
    {
        //InitializePoolSpawnPoints(); // is called once before the pool is populated. MUST BE CALLED BEFORE POOL POPULATION, as if called, after, the enemies will all be added as spawn points too.
        enemyPoolManagerScript = GetComponentInParent<Enemy_Pool_Manager>();
        
    }

    private void OnEnable()
    {
        SpawnTimerHitZero += ActivateEnemyOnSpawnRate;
    }

    void Start()
    {
        EventSubscriptions();
        InitialPoolSetup();
     /*   if (canPopulatePool)
        {
            
        }
        else
        {
            Debug.LogError("PopulatePool() in Enemy_Object_Pool not called as the pool's spawn points weren't initially set first. \n" +
                "InitializePoolSpawnPoints() must be called before PopulatePool()");
        }*/
    }

    private void Update()
    {
        SpawnCountdownTimer();
    }

    // Spawning Methods

    private void SpawnCountdownTimer() 
    {
        if (spawnTimerActive == true)
        {
            SpawnTimer -= Time.deltaTime;
        }
    }

    private void ActivateEnemyOnSpawnRate()
    {
        GameObject enemyToActivate = GetNextObject(arrayControl);
        var cond = arrayControl == (pooledObjects.Length - 1) ? arrayControl = 0 : arrayControl++;
      

        enemyToActivate.transform.position = enemyPoolManagerScript.SpawnPoints[UnityEngine.Random.Range(0, enemyPoolManagerScript.SpawnPoints.Count)].position;
        enemyToActivate.SetActive(true);

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
        PopulatePool();
        spawnTimerActive = true;

        enemyPoolManagerScript.SpawnPointsInitialized -= InitialPoolSetup;
    }

    protected override void PopulatePool()
    {
        Debug.Log("Enemy Object Pool: Populate Object pool called");

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

            localEnemyScript.SetUpEnemy(waveMovementSpeed : enemyWaveData.EnemySpeed, shape_Info: enemyWaveData.EnemyShape, enemyMovementTarget: waveTarget); // Changed this m,ethod to only call for the shape info once, as calliong for sprite and type 
            // separately was causing 2 random.range calculations, leading to mismatched data in each property call.  
            
            pooledObjects[i].SetActive(false);
        }
    }

   /* private void InitializePoolSpawnPoints()
    {   
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                spawnPoints.Add(transform.GetChild(i).transform);
            }
        }
        else 
        { 
            Debug.Log("Enemy Spawner contains no spawn points as children."); 
            GameObject.Instantiate(new GameObject("SpawnPointCreated"), gameObject.transform); 
            spawnPoints.Add(transform.GetChild(0)); 
        }

        canPopulatePool = true;
    }*/

    // Internal Script Logic Methods

   /* protected override GameObject GetNextObject()
    {
        GameObject returnedEnemy = pooledObjects[arrayControl];
        if (arrayControl == pooledObjects.Length - 1)
        {
            arrayControl = 0;
            return returnedEnemy;
        }
        else { 
            arrayControl++; }
        
        
        return returnedEnemy;
    }*/

    private void EventSubscriptions()
    {
        enemyPoolManagerScript.SpawnPointsInitialized += InitialPoolSetup; 
        
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
    private void OnDisable()
    {
        SpawnTimerHitZero -= ActivateEnemyOnSpawnRate;
    }
}
