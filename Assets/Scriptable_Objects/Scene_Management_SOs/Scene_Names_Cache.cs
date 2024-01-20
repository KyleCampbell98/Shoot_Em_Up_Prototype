using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SceneList"))]
public class Scene_Names_Cache : ScriptableObject
{
    public enum Scenes { _menu, gameplay };
    private const string main_Menu = "Startup_Menu_Scene";
    
    private const string gameplay_Scene = "GameScene";
   

    public  string ReturnConstSceneName(Scenes scene) // 20/01/24 changed this method to be static, may cause issues but i dont yet know. Now nothing in this SO needs a script reference
    {
        string sceneToReturnForLoad;
        switch (scene)
        {

            case Scenes.gameplay:
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
