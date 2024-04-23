using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public void DisableOnAnimEnd()
    {
        gameObject.SetActive(false);
    }
}
