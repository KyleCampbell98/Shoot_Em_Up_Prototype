using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instructions_Scene_UI_Event_Control : UI_Event_Control
{
    [SerializeField] private GameObject closeInstructionsTabButton;
    [SerializeField] private GameObject openInstructionsTabButton;

    public void OnControlPanelShow(bool selected)
    {
        if (selected)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(closeInstructionsTabButton);
        }
        else if (!selected)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(openInstructionsTabButton);
        }
    }
}
