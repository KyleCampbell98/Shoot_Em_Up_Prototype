using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// information about shape cutouts stored in a SO due to it needing to be referenced by both the player's weapon, and the enemies.
/// If the enemy and player can reference the same parameters for setting up these shapes, they can be setup to match each other programatically.
/// Anything changed in the player's projectile such as the size of it, will be automatically mirrored by the enemy as they will both be referencing
/// the same object details. 
/// </summary>

public class Cutout_Shape : ScriptableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
