using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaRefManager : MonoBehaviour
{
    [Header("Debug Controls")]
    [SerializeField] private static bool showDebug = false;

    private static GameObject playAreaBounds;
    public static GameObject PlayAreaBounds 
    { get 
        {
            if (playAreaBounds != null) { numberOfStaticVariableRetrievals++; }
            if (showDebug) { Debug.Log($"{debugStatementTag} Play area bounds collider retrieved: {numberOfStaticVariableRetrievals} times"); }
            return playAreaBounds; 
        } 
        private set { playAreaBounds = value; } 
    }

    private const string debugStatementTag = "PlayAreaRefManager Script: ";

    private static int numberOfStaticVariableRetrievals = 0;

    private void Awake()
    {
        if (PlayAreaBounds == null)
        {
            if (GetComponentInChildren<Collider2D>() != null)
            {
            //    Debug.Log($"{debugStatementTag}Play Area Bounds Collider set.");
                PlayAreaBounds = this.gameObject;
            }
            else
            {
                Debug.LogError($"{debugStatementTag}PLAY AREA BOUNDS COLLIDER NOT SET. NO COLLIDER FOUND IN CHILDREN OBJECTS");
            }
        }
    }
}
