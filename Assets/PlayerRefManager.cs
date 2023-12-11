using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefManager : MonoBehaviour
{
    private static GameObject selfReference;
    public static GameObject PlayerReference { get { return selfReference; } private set { selfReference = value; } }

    // Start is called before the first frame update
    void Start()
    {
        PlayerReference = this.gameObject;
    }

   
}
