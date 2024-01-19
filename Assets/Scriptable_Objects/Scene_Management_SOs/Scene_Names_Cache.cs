using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Names_Cache : ScriptableObject
{
    public enum Scenes { _menu, _gameplay };
    private const string main_Menu = "Startup_Menu_Scene";
    
    private const string gameplay_Scene = "GameScene";
   

    public string ReturnConstSceneName(Scenes scene)
    {
        string sceneToReturnForLoad;
        switch (scene)
        {

            case Scenes._gameplay:
                sceneToReturnForLoad = gameplay_Scene;
                break;

            case Scenes._menu:
                sceneToReturnForLoad = main_Menu;
                break;

            default:
                sceneToReturnForLoad = main_Menu;
                Debug.LogError($"Scene: \"{scene}\" doesn't exist in the build settings. Loading Menu by default."); ;
                break;
        }

        return sceneToReturnForLoad;
    }
}
