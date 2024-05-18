using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Event_Control : MonoBehaviour
{
    [SerializeField] protected GameObject initialButtonSelection;
    [SerializeField] private GameObject lastSelectedObjectBeforeFocusLoss;
    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("Event system start called");
        SetInitialButtonSelection();
    }

    protected void OnApplicationFocus(bool focus)
    {
        if(focus) 
        {
            Debug.Log("Focus to app returned");

            ResetCurrentSelectedObject(lastSelectedObjectBeforeFocusLoss);
            
        }
        else
        {
            lastSelectedObjectBeforeFocusLoss = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    protected void SetInitialButtonSelection()
    {
        Debug.Log("Initial Button Selection Called");
        ResetCurrentSelectedObject(initialButtonSelection);
    }

    protected void ResetCurrentSelectedObject(GameObject targetObjectSelection)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(targetObjectSelection);
    }
}
