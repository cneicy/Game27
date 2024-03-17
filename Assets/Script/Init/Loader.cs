using System;
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
                case Scene.Level2:
                    break;
                case Scene.Level3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
        }
    }
}