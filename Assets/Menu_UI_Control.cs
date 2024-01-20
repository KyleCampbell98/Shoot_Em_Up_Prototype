using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_UI_Control : MonoBehaviour
{
    [SerializeField] private Scene_Names_Cache _Cache;

   public void OnButtonClicked(GameObject buttonBeingClicked)
    {
        Debug.Log(buttonBeingClicked.name);

        if (buttonBeingClicked.GetComponent<Button_Utility>() != null) 
        {
            SceneManager.LoadScene(_Cache.ReturnConstSceneName(buttonBeingClicked.GetComponent<Button_Utility>().SceneAssociatedWithThisButton));
        }
        else { Debug.LogError("BUTTON DOES NOT CONTAIN A BUTTON UTILITY CLASS. ONE MUST BE ATTATCHED TO ASSIGN AN ENUM FOR UI CONNECTIVITY"); }
    }

    public void OnClickTest(int i)
    {

    }
}
