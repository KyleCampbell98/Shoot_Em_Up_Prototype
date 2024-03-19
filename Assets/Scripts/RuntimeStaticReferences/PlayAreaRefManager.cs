using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaRefManager : MonoBehaviour
{
    private static GameObject playAreaBounds;
    public static GameObject PlayAreaBounds  { get { return playAreaBounds; } private set { playAreaBounds = value; } }

    private const string debugStatementTag = "PlayAreaRefManager Script: ";


    /// <summary>
    /// 12/12/23 Could probably do with adding a singleton pattern check to ensure that this is the only instance of the Screen_Bound_Parent object in the scene, 
    /// to ensure that the static reference set here isnt set multiple times. 
    /// </summary>

    private void Awake()
    {
        if (PlayAreaBounds == null)
        {
            if (GetComponentInChildren<Collider2D>() != null)
            {
                //    Debug.Log($"{debugStatementTag}Play Area Bounds Collider set.");
                PlayAreaBounds = GetComponentInChildren<Collider2D>().gameObject;
            }
            else
            {
                Debug.LogError($"{debugStatementTag}PLAY AREA BOUNDS COLLIDER NOT SET. NO COLLIDER FOUND IN CHILDREN OBJECTS");
            }
        }
    }

    
}
