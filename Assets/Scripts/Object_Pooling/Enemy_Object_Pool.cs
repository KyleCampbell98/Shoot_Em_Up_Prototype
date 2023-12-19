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

    void Start()
    {
        InitialPoolSetup();
        PopulatePool();
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

            localEnemyScript.SetUpEnemy(enemyWaveData.EnemySpeed, enemyWaveData.WaveShapeType, enemyWaveData.EnemySprite, waveTarget);
            
            pooledObjects[i].SetActive(false);
        }
    }

    private IEnumerator SequentialEnemySpawner()
    {

        GetPooledObject().SetActive(true);

        yield return new WaitForSeconds(enemyWaveData.SpawnRate);
        StartCoroutine(nameof(SequentialEnemySpawner));
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
}
