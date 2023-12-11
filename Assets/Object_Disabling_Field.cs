using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Disabling_Field : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
    }
}
