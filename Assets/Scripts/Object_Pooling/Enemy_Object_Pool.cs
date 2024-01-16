using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Enemy_Object_Pool : Object_Pool_Template
{
    [SerializeField] private Enemy_Wave_SO_Template enemyWaveData;
    [SerializeField] private GameObject waveTarget;
    
    private Collider2D waveAreaTarget; // For use in setting a reference to the general play area.
    [SerializeField] private float spawnTimer;
    void Start()
    {
        InitialPoolSetup();
        PopulatePool();
        spawnTimer = enemyWaveData.SpawnRate;
    }
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0) 
        {
            GetPooledObject().SetActive(true);
            spawnTimer = enemyWaveData.SpawnRate;
        }
    }

    private void InitialPoolSetup()
    {
        objectToPool = enemyWaveData.EnemyToSpawn;
        objectPoolSize = enemyWaveData.EnemyPoolSize;
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

    private IEnumerator SequentialEnemySpawner()
    {

       GetNextObject(arrayControl).SetActive(true);

        yield return new WaitForSeconds(enemyWaveData.SpawnRate);
        StartCoroutine(SequentialEnemySpawner());
        Debug.Log("Reached here during coroutine.");
    } // Initial enemy spawning test.

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
}
