using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Object_Pool : Object_Pool_Template
{
    [SerializeField] private Transform firePoint;
    private delegate void PassProjectileParameters(Cutter_And_Enemy_Shape_Enums.ShapeType shapeOfProj, Sprite spriteOfProj);
    private PassProjectileParameters _passProjectileParameters;

    private Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeType;
    [SerializeField] private Sprite currentShapeSelectionSprite;

    [SerializeField] private Sprite[] sprites = new Sprite[Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length];

    private void Start()
    {
        SetTransformCachedVariables();
        SubscribeToFireEvent();
        PopulatePool(); // bullet pool
        
    }

    protected override void PopulatePool()
    {
        base.PopulatePool();
        foreach(GameObject projectile in pooledObjects)
        {
            if(projectile.GetComponent<Player_Shape_Projectile_Logic>() != null)
            {
                _passProjectileParameters += projectile.GetComponent<Player_Shape_Projectile_Logic>().SetupProjectile;
               
            }
        }
    }

    private void SetTransformCachedVariables()
    {
        pooledObjectParent = Player_Projectile_Parent.PlayerProjectileParentReference.transform;
        firePoint = gameObject.transform;
    }
    private void SetCurrentShape(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeSelection)
    {
        Debug.Log("SET CURRENT SHAPE CALLED");
        currentShapeType = currentShapeSelection;

        switch (currentShapeType)
        {
            case Cutter_And_Enemy_Shape_Enums.ShapeType.Circle:

                currentShapeType = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
                currentShapeSelectionSprite = sprites[0];

                break;
            case Cutter_And_Enemy_Shape_Enums.ShapeType.Triangle:

                currentShapeType = Cutter_And_Enemy_Shape_Enums.ShapeType.Triangle;
                currentShapeSelectionSprite = sprites[1];

                break;
            case Cutter_And_Enemy_Shape_Enums.ShapeType.Square:

                currentShapeType = Cutter_And_Enemy_Shape_Enums.ShapeType.Square;
                currentShapeSelectionSprite = sprites[2];

                break;
        }
    }
    private void SubscribeToFireEvent()
    {
        if (gameObject.GetComponentInParent<New_Input_System_Controller>() != null)
        {
            this.GetComponentInParent<New_Input_System_Controller>().OnFireHit += ActivateBullets;
            //Debug.LogError("YEYE");
        }
        else { Debug.LogError("PARENT OBJECT MISSING CONTROLLER SCRIPT"); }

        if (gameObject.GetComponent<Player_Shape_Cutter>() != null)
        {
            this.GetComponent<Player_Shape_Cutter>()._selectedShapeEnum += SetCurrentShape;
            //Debug.LogError("YEYE");
        }
    }

    private void ActivateBullets()
    {
        //   Debug.Log("Activate Bullets in Player Object Pool Called");
       

        GameObject shapeProjectile = GetPooledObject();
        if(shapeProjectile != null)
        {
            _passProjectileParameters.Invoke(currentShapeType, currentShapeSelectionSprite);
            shapeProjectile.transform.SetPositionAndRotation(firePoint.transform.position, transform.rotation.normalized);
            
            shapeProjectile.SetActive(true);
        }
        else { Debug.LogWarning("Could not retrieve object to enable from pool."); }
    }

    private void OnDisable()
    {
     //   GetComponentInParent<New_Input_System_Controller>().OnFireHit -= ActivateBullets;
     ///* This code can't run as a disabled object can't retrieve a component.Need to have a separate function that
     ///unsubs from all events, before finalising the player destruction/disabling.
     ///*


    }
}
