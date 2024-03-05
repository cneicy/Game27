using System.Collections;
using UnityEngine;

namespace Script.Init
{
    public class Init : MonoBehaviour
    {
        public static Loader.Scene Scene;
        //加载中间页
        public static void InitScene(Loader.Scene scene)
        {
            Scene = scene;
        }
        private void Awake()
        {
            StartCoroutine(nameof(WaitForLoad));
        }

        private IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(2);
            Loader.Load(Scene);
        }
    }
}