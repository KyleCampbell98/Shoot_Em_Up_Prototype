using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Static_Helper_Methods : MonoBehaviour
{
    public static T FindComponentInGameObject<T>(GameObject @object) where T : Component // Searches through all elements of a game object for a specific component
    { // Method not suitable for finding multiple instances of the same component type on a game object


       // bool objectHasOneOfComponentType = CountNumberOfComponentInObject<T>(@object);
       // if(!objectHasOneOfComponentType) { return null; }
        
  
        if (@object.GetComponent<T>() != null)
        {
            return @object.GetComponent<T>();
        }
        else if (@object.GetComponentInParent<T>() != null)
        {
            return @object.GetComponentInParent<T>();
        }
        else if (@object.GetComponentInChildren<T>() != null)
        {
            return @object.GetComponentInChildren<T>();
        }
        else if (@object.transform.parent.GetComponentInChildren<T>() != null) 
        {
            return @object.transform.parent.GetComponentInChildren<T>();
        }

        Debug.LogError($"NO COMPONENT OF TYPE {typeof(T)} FOUND IN OBJECT {@object.name.ToString()}");
        return null;


    }

    // Internal Script Logic
    private static bool CountNumberOfComponentInObject<T>(GameObject objectToCheck)
    {

        int numberOfComponentTypeInObject = objectToCheck.transform.parent.GetComponents<T>().Length;
        for (int i = 0; i < objectToCheck.transform.parent.childCount; i++)
        {
            numberOfComponentTypeInObject += objectToCheck.transform.parent.GetChild(i).GetComponents<T>().Length;
        }
        if (numberOfComponentTypeInObject > 1)
        {
            Debug.LogError($"{objectToCheck} contains more than 1 component of type {typeof(T)}. Method only applies to objects containing 1 of the specified component in the Parent-Child Hierachy");
            return false;
        }
        return true;
    }
}
