using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class Menu_UI_Control : MonoBehaviour
{
    [SerializeField] private Scene_Names_Cache _Cache;
   
    // Button Methods
    public void OnButtonClicked(GameObject buttonBeingClicked)
    {

        if (buttonBeingClicked.GetComponent<Button_Utility>() != null) 
        {
            if (buttonBeingClicked.GetComponent<Button_Utility>().IsQuitButton) { Debug.Log("Quitting Application..."); Application.Quit(); return; }
            else if(buttonBeingClicked.GetComponent<Button_Utility>().IsContinueButton) { Debug.Log("Continuing Game"); GameManager.a_ActivatePause(); return; }
            
            SceneManager.LoadScene(_Cache.ReturnConstSceneName(buttonBeingClicked.GetComponent<Button_Utility>().SceneAssociatedWithThisButton));
        }
        else { Debug.LogError("BUTTON DOES NOT CONTAIN A BUTTON UTILITY CLASS. ONE MUST BE ATTATCHED TO ASSIGN AN ENUM FOR UI CONNECTIVITY"); }
    }

    public void OnClickTest(int i)
    {

    }

    protected string FormatRawTime(float rawTimeData)
    {
        int minutes = Mathf.FloorToInt(rawTimeData / 60F);
        int seconds = Mathf.FloorToInt(rawTimeData - (minutes * 60));

        return string.Format("{0:00}:{1:00}", minutes, seconds); // COPY PASTE CODE
    }
}
