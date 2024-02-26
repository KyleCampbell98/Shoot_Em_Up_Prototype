using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Object_Pool : Object_Pool_Template
{
    [Header("Scriptable Objects for setup/UI use")]
    [SerializeField] private InPlay_Details game_Session;

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
        SetProjectileShapeDetails(Cutter_And_Enemy_Shape_Enums.ShapeType.Circle);
        
    }

    protected override void PopulatePool()
    {
        objectPoolSize = game_Session.StartingPlayerBombs;
        base.PopulatePool();
        foreach(GameObject projectile in pooledObjects)
        {
            projectile.transform.position = pooledObjectParent.transform.position;
            
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
    private void SetProjectileShapeDetails(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeSelection)
    {
        //Debug.Log("SET CURRENT SHAPE CALLED");
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
    } // Passes into as projectile the: Shape enum so enemies can detect the projectile, and the sprite to use as a visual representation as to what the projectile actually is. 
    private void SubscribeToFireEvent()
    {
        if (gameObject.GetComponentInParent<New_Input_System_Controller>() != null)
        {
            this.GetComponentInParent<New_Input_System_Controller>().OnFireHit += ActivateBullets;         
        }
        else { Debug.LogError("PARENT OBJECT MISSING CONTROLLER SCRIPT"); }

        if (gameObject.GetComponent<Player_Shape_Cutter>() != null)
        {
            this.GetComponent<Player_Shape_Cutter>()._selectedShapeEnum += SetProjectileShapeDetails;
        }
        else { Debug.LogError("PLAYER_SHAPE_CUTTER IS MISSING. PLAYER MUST HAVE THIS TO ASSIGN PROJECTILES SHAPE DATA."); }
    }

    private void ActivateBullets()
    {
        GameObject shapeProjectile = null;

        shapeProjectile = Array.Find(pooledObjects, gameObject => gameObject.activeSelf == false);
        if(shapeProjectile == null)
        {
            Debug.Log("No Ammo left!");
            return;
        }
      

        if (shapeProjectile != null)
        {
            _passProjectileParameters.Invoke(currentShapeType, currentShapeSelectionSprite);
            shapeProjectile.transform.SetPositionAndRotation(firePoint.transform.position, transform.rotation.normalized);
            
            shapeProjectile.SetActive(true);
            game_Session.BombsRemaining--;
            GameManager.a_playerValuesUpdated();
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
