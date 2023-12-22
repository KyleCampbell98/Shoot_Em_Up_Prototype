using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Disable_Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] children;

    private void Awake()
    {
        children = new GameObject[transform.childCount];
        if (children != null)
        {
            Debug.Log($"Child Count: {transform.childCount}");
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i).gameObject;

            }
        }
    }

    private void OnDisable()
    {
        
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }

        
    }
}
