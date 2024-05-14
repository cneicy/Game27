﻿using System.Collections;
using Script.Init.UI;
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

        public static void InitScene(int scene)
        {
            Scene = (Loader.Scene)scene;
        }

        private void Awake()
        {
            StartCoroutine(nameof(WaitForLoad));
        }

        private IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(5);
            GameObject.FindWithTag("Tip").GetComponent<Tip>().Roll();
            Loader.Load(Scene);
        }
    }
}