using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shape_Projectile_Logic : MonoBehaviour
{
    [Header("Object Control Attributes")]
    [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType thisProjectilesShapeType; // sets shape type on each activation from the pool.
    [SerializeField] private Sprite thisObjectsSpriteShape;
    private bool canGameOverPlayer = false; // Should be false upon placing the projectile, could be done through a trigger behaviour, or a timer?
    [SerializeField] private bool callShapeSetupLogic = true;

    [Header("Component References")]
    [SerializeField] private SpriteRenderer projectileSpriteRenderer; // Used to set sprite on each activation from the pool. 
    [SerializeField] private Animator projectileAnimController;
    [SerializeField] private Collider2D projectileCollider;
    [SerializeField] private Transform projectileTransformParent; // Get in the player object pool script? So then the reference is only being retrieved once, then pass into each projectile
    // from the object pool. Otherwise, each object is going to be spawning on the object pool creation, and getting the same static reference for caching. 
   
    // Timer attributes
    [SerializeField] private float timeBeforeActivatingDanger; // This is the amount of seconds after being placed that the danger to the player of their own projectile will be instated.
    private const string activatedBombStringConst = "_Activated";
    // Object Control Properties
    public Cutter_And_Enemy_Shape_Enums.ShapeType ProjectilesShapeType { get { return thisProjectilesShapeType; } 
        set 
        { // Need To ensure this is only called on the projectile's activation. 
            thisProjectilesShapeType = value; 
        } 
    }

    public Action a_ResetBullet;

    private void Awake()
    {
        projectileSpriteRenderer = GetComponent<SpriteRenderer>();
        projectileCollider = GetComponent<Collider2D>();
        projectileAnimController = GetComponent<Animator>();
        projectileTransformParent = gameObject.transform.parent;
    }

    private void OnEnable()
    {
        OnEnableLogic();
        StartCoroutine(ActivateDanger());
    }

    public void SetupProjectile(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeType, Sprite currentPlayerSprite) // This needs to be subbed to an event in the projectile pool (fired when projectile activation is called)
    {
        if (!callShapeSetupLogic) { return; }
        thisProjectilesShapeType = currentShapeType;
        thisObjectsSpriteShape = currentPlayerSprite;
    }

    private void OnDisable()
    {
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        transform.position = projectileTransformParent.position;
        callShapeSetupLogic = true;
        canGameOverPlayer = false;
        projectileSpriteRenderer.sprite = null;
        // Need to reset chain of animations so that projectile returns to unactivated state
        transform.position = gameObject.transform.parent.position;
        projectileCollider.enabled = false;
        a_ResetBullet?.Invoke();
    }

    private IEnumerator ActivateDanger()
    {
        yield return new WaitForSeconds(timeBeforeActivatingDanger);
        canGameOverPlayer = true;
        Audio_Manager.PlaySoundStatic(Audio_Manager.SoundNames.bomb_active);
        projectileAnimController.SetTrigger(thisProjectilesShapeType.ToString() + activatedBombStringConst);
        Debug.Log(thisProjectilesShapeType + activatedBombStringConst);
        // Need Logic for triggering the activation animation for the player's bomb
        projectileCollider.enabled = true;
    }

    private void OnEnableLogic()
    {
        callShapeSetupLogic = false; // Stops individual shape being overriden by the setup of other shapes in the object pool.
        
        projectileSpriteRenderer.sprite = thisObjectsSpriteShape;
        projectileAnimController.SetBool(thisProjectilesShapeType.ToString(), true);
        projectileAnimController.Play("Decider_State");
    }
}
