using System;
using System.Collections;
using Script.Init;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Script.Game.Object
{
    public class EndPoint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const int TargetAlpha = 255;
        private bool _loadTrigger;
        private AudioSource _audioSource;
        private GameObject _player;
        [SerializeField] private Loader.Scene scene;
        private bool isEnded;
        private Rigidbody2D _rigidbody2D;
        private Light2D _light2D;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _light2D = _player.GetComponentInChildren<Light2D>();
            _rigidbody2D = _player.GetComponent<Rigidbody2D>();
            
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _audioSource.Play();
            if (Init.Init.Scene != Loader.Scene.Level4)
            {
                _loadTrigger = true;
                StartCoroutine(Next());
            }
            else
            {
                _player.GetComponent<Rigidbody2D>().gravityScale = -1;
                spriteRenderer.color = Color.white;
                isEnded = true;
            }
        }

        private void EndGame()
        {
            
            var temp = _rigidbody2D.velocity;
            temp.x = Mathf.Clamp(temp.x, 0, 0);
            temp.y = 2;
            _rigidbody2D.velocity = temp;
            _light2D.intensity+=0.001f;
            _light2D.pointLightOuterRadius+=0.1f;
            /*_light2D.intensity = 0;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            spriteRenderer.color = color;*/
        }
        private IEnumerator Next()
        {
            yield return new WaitForSecondsRealtime(1);
            Init.Init.InitScene(scene);
            SceneManager.LoadScene("Init");
        }
        private void FixedUpdate()
        {
            if(isEnded) EndGame();
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            spriteRenderer.color = color;
        }
    }
}