using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Object_Pool : Object_Pool_Template
{

    private void Awake()
    {
        pooledObjectParent = this.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.PopulatePool();
        EventSubscriptions();
    }

    private void EnablePowerUpFromPool()
    {
        base.GetPooledObject().SetActive(true);
    }

    private void EventSubscriptions()
    {
        GameManager.a_ReleaseHPPickupDrop += EnablePowerUpFromPool;
    }

    private void OnDisable()
    {
        GameManager.a_ReleaseHPPickupDrop -= EnablePowerUpFromPool;
    }
}

