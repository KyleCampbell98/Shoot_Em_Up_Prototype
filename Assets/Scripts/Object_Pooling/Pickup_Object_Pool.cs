using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Object_Pool : Object_Pool_Template
{
    private Collider2D spawnArea;
    

    private void Awake()
    {
        spawnArea = Static_Helper_Methods.FindComponentInGameObject<Collider2D>(FindObjectOfType<PlayAreaRefManager>().gameObject);
        pooledObjectParent = this.gameObject.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.PopulatePool();
        EventSubscriptions();
    }

    private void EnablePowerUpFromPool()
    {
        GameObject powerUpToEnable = base.GetPooledObject();
        powerUpToEnable.transform.position = RandomTargetGenerator();
        powerUpToEnable.SetActive(true);
        
    }

    private void EventSubscriptions()
    {
        GameManager.a_ReleaseHPPickupDrop += EnablePowerUpFromPool;
    }

    private void OnDisable()
    {
        GameManager.a_ReleaseHPPickupDrop -= EnablePowerUpFromPool;
    }

    private Vector2 RandomTargetGenerator()
    {
        Vector2 newTarget;

        newTarget = new Vector2(UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
        return newTarget;
    }
}

