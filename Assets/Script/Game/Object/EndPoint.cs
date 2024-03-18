using System.Collections;
using Script.Init;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game.Object
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const int TargetAlpha = 255;
        private bool _loadTrigger;
        private void OnTriggerEnter2D(Collider2D other)
        {
            _loadTrigger = true;
            StartCoroutine(Next());
        }

        private IEnumerator Next()
        {
            yield return new WaitForSecondsRealtime(1);
            Init.Init.InitScene(Loader.Scene.Level2);
            SceneManager.LoadScene("Init");
        }
        private void FixedUpdate()
        {
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            
            spriteRenderer.color = color;
        }
    }
}