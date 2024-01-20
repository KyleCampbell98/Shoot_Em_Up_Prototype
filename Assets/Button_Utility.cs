using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Utility : MonoBehaviour
{
    /// <summary>
    /// EVERY BUTTON OBJECT MUST CONTAIN ONE OF THESE UTILITY SCRIPTS
    /// </summary>

    [SerializeField] private Scene_Names_Cache.Scenes sceneAssociatedWithThisButton;
    public Scene_Names_Cache.Scenes SceneAssociatedWithThisButton {  get { return sceneAssociatedWithThisButton; } }
}
