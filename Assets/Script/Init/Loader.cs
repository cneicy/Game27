using UnityEngine.SceneManagement;

namespace Script.Init
{
    public abstract class Loader
    {
        public enum Scene
        {
            Level1, Level2, Level3, MainMenu
        }

        public static void Load(Scene scene)
        {
            switch (scene)
            {
                case Scene.Level1:
                    SceneManager.LoadScene("Game");
                    break;
                case Scene.MainMenu:
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }
    }
}