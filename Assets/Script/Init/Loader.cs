using System;
using UnityEngine.SceneManagement;

namespace Script.Init
{
    public abstract class Loader
    {
        public enum Scene
        {
            Level1,
            Level2,
            Level3,
            Level4,
            MainMenu,
            Credit,
            Stop
        }

        public static void Load(Scene scene)
        {
            switch (scene)
            {
                case Scene.Level1:
                    SceneManager.LoadScene(2);
                    break;
                case Scene.MainMenu:
                    SceneManager.LoadScene("MainMenu");
                    break;
                case Scene.Level2:
                    SceneManager.LoadScene(3);
                    break;
                case Scene.Level3:
                    SceneManager.LoadScene(4);
                    break;
                case Scene.Level4:
                    SceneManager.LoadScene(5);
                    break;
                case Scene.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scene), scene, null);
            }
        }
    }
}