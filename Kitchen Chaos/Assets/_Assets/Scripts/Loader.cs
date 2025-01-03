using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene 
    {
        MainMenu,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;
    
    public static void Load(Scene targetSceneName) 
    {
        Loader.targetScene = targetSceneName;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}