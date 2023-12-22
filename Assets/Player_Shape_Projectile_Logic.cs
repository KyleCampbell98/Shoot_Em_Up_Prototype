using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shape_Projectile_Logic : MonoBehaviour
{
    // Object Control Attributes
    private Cutter_And_Enemy_Shape_Enums.ShapeType? thisProjectilesShapeType; // sets shape type on each activation from the pool.
    private bool canGameOverPlayer; // Should be false upon placing the projectile, could be done through a trigger behaviour, or a timer?

    // Component References
    private SpriteRenderer projectileSpriteRenderer; // Used to set sprite on each activation from the pool. 

    // Timer attributes
    private float timeBeforeActivatingDanger; // This is the amount of seconds after being placed that the danger to the player of their own projectile will be instated.

    // Object Control Properties
    public Cutter_And_Enemy_Shape_Enums.ShapeType? ProjectilesShapeType { get { return thisProjectilesShapeType; } 
        set 
        { // Need To ensure this is only called on the projectile's activation. 
            thisProjectilesShapeType = value; 
        } 
    }

    private void OnDisable()
    {
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        projectileSpriteRenderer.sprite = null;

    }
}
