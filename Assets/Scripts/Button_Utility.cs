using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Utility : MonoBehaviour
{
    /// <summary>
    /// EVERY BUTTON OBJECT MUST CONTAIN ONE OF THESE UTILITY SCRIPTS
    /// </summary>

    [SerializeField] bool isQuitButton = false;
    [SerializeField] bool isContinueButton = false;
    [SerializeField] private Scene_Names_Cache.Scenes sceneAssociatedWithThisButton;
    public Scene_Names_Cache.Scenes SceneAssociatedWithThisButton {  get { return sceneAssociatedWithThisButton; } }
    public bool IsQuitButton { get { return isQuitButton; } }
    public bool IsContinueButton { get { return isContinueButton; } }


}
