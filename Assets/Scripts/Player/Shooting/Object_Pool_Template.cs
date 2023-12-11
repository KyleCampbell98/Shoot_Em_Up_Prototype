using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool_Template : MonoBehaviour
{
    [Header("Object Pool Configs")]
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int objectPoolSize;
    [SerializeField] protected GameObject[] pooledObjects;
    [SerializeField] protected Transform pooledObjectParent;

    protected virtual void PopulatePool()
    {
        Debug.Log("Populate Object pool called");

        pooledObjects = new GameObject[objectPoolSize];
        for (int i = 0; i < objectPoolSize; i++)
        {
            pooledObjects[i] = Instantiate(objectToPool, transform.position, Quaternion.identity, pooledObjectParent);
            pooledObjects[i].SetActive(false);
        }
    }
    protected GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPoolSize; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        Debug.LogWarning("No Objects available. Consider increasing object pool size");
        return null;
    }

    protected void SetObjectParentToSelf()
    {
        pooledObjectParent = this.gameObject.transform;
    }
}
