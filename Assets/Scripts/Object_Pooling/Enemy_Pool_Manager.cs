using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pool_Manager : MonoBehaviour
{
    // Spawning Parameters
    private List<Transform> spawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPoints();
    }

    private void InitializeSpawnPoints()
    {
        var list = GetComponentsInChildren<Enemy_Spawn_Point_Ref>();
        foreach (var p in list)
        {
            spawnPoints.Add(p.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
