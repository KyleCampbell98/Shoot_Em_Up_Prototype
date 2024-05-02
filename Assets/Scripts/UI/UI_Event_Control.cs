using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Event_Control : MonoBehaviour
{
    [SerializeField] private GameObject initialButtonSelection;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialButtonSelection();
    }

    private void SetInitialButtonSelection()
    {
        EventSystem.current.SetSelectedGameObject(null); // Good practice to set null before resetting to anything else;
        EventSystem.current.SetSelectedGameObject(initialButtonSelection);
    }
}
