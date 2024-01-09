using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile_Parent : MonoBehaviour
{
    private static GameObject playerProjectileParentSelfReference;
    public static GameObject PlayerProjectileParentReference { get { return playerProjectileParentSelfReference; } private set { playerProjectileParentSelfReference = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        playerProjectileParentSelfReference = this.gameObject;
    }
}
