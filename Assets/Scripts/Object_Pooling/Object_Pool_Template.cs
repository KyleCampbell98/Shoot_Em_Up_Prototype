using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object_Pool_Template : MonoBehaviour
{
    [Header("Object Pool Configs")]
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int objectPoolSize;
    [SerializeField] protected GameObject[] pooledObjects;
    [SerializeField] protected Transform pooledObjectParent;

    [Header("Pool Object Control")]
    [SerializeField] protected int arrayControl = 0;

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
    protected virtual GameObject GetPooledObject() // Potentially override this so that once all enemies are active, "Populate Pool" is recalled with a new set of enemy data to be setup.
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

    protected virtual GameObject GetNextObject(int indexer) 
    {
        GameObject returnedObj = pooledObjects[indexer];
       
     
        return returnedObj;
        
    } // All object pools must have a method of getting the next object from their pool in sequence
    protected bool CheckIfNeededObjectActive(int indexOfObjToCheck)
    {
        bool objectIsActive = pooledObjects[indexOfObjToCheck].activeSelf ? true : false;
        return objectIsActive;
    }
  
    protected void SetObjectParentToSelf()
    {
        pooledObjectParent = this.gameObject.transform;
    }
}
