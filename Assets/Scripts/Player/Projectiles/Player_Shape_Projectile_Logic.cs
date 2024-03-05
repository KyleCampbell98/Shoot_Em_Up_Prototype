using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shape_Projectile_Logic : MonoBehaviour
{
    // Object Control Attributes
    private Cutter_And_Enemy_Shape_Enums.ShapeType? thisProjectilesShapeType; // sets shape type on each activation from the pool.
    private bool canGameOverPlayer = false; // Should be false upon placing the projectile, could be done through a trigger behaviour, or a timer?
    [SerializeField] private bool callShapeSetupLogic = true;

    // Component References
    [SerializeField] private SpriteRenderer projectileSpriteRenderer; // Used to set sprite on each activation from the pool. 
    [SerializeField] private Animator projectileAnimController;
    [SerializeField] private Collider2D projectileCollider;
    [SerializeField] private Transform projectileTransformParent; // Get in the player object pool script? So then the reference is only being retrieved once, then pass into each projectile
    // from the object pool. Otherwise, each object is going to be spawning on the object pool creation, and getting the same static reference for caching. 
   
    // Timer attributes
    [SerializeField] private float timeBeforeActivatingDanger; // This is the amount of seconds after being placed that the danger to the player of their own projectile will be instated.

    // Object Control Properties
    public Cutter_And_Enemy_Shape_Enums.ShapeType? ProjectilesShapeType { get { return thisProjectilesShapeType; } 
        set 
        { // Need To ensure this is only called on the projectile's activation. 
            thisProjectilesShapeType = value; 
        } 
    }

    private void Awake()
    {
        projectileSpriteRenderer = GetComponent<SpriteRenderer>();
        projectileCollider = GetComponent<Collider2D>();
        projectileAnimController = GetComponent<Animator>();
        projectileTransformParent = gameObject.transform.parent;
    }

    private void OnEnable()
    {
        callShapeSetupLogic = false; // Stops individual shape being overriden by the setup of other shapes in the object pool.
        StartCoroutine(ActivateDanger());
    }

    public void SetupProjectile(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeType, Sprite currentPlayerSprite) // This needs to be subbed to an event in the projectile pool (fired when projectile activation is called)
    {
        if (!callShapeSetupLogic) { return; }
        thisProjectilesShapeType = currentShapeType;
        Debug.Log("PROJECTILE SETTING ANIM TRIGGER OF: " + currentShapeType.ToString());
        projectileAnimController.SetTrigger(currentShapeType.ToString());
        projectileSpriteRenderer.sprite = currentPlayerSprite;  
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
    }

    private IEnumerator ActivateDanger()
    {
        yield return new WaitForSeconds(timeBeforeActivatingDanger);
        canGameOverPlayer = true;
        // Need Logic for triggering the activation animation for the player's bomb
        projectileCollider.enabled = true;
    }
}
