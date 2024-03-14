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
    }
    
    }

