using System;
using System.Collections;
using System.Collections.Generic;
using Script.Init;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Script.Game.Object
{
    public class EndPoint : NetworkBehaviour
    {
        private const int TargetAlpha = 255;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] public Loader.Scene scene;
        [SerializeField] private AudioSource bgm;

        private AudioSource _audioSource;
        private GameObject _player;
        private Rigidbody2D _rigidbody2D;
        private Light2D _light2D;
        [SerializeField] private int index;

        private bool _bgmOut;
        private bool _loadTrigger;
        private bool _isEnded;
        
        private void Awake()
        {
            NetworkManager.Singleton.StartClient();
        }
        
        private void Start()
        {
            NetworkManager.Singleton.OnServerStarted += Load;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<Player.Player>().IsOwner) return;
            //检查是否为第四关结束 如果为第四关结束则执行结束逻辑 如果不是则执行下一关加载
            if (Init.Init.Scene != Loader.Scene.Level4)
            {
                _loadTrigger = true;
                StartCoroutine(Next());
            }
            else
            {
                GameObject.FindWithTag("Moon").SetActive(false);
                _isEnded = true;
                StartCoroutine(Next());
            }
        }

        private void SilentAll()
        {
            var temp = GameObject.FindGameObjectsWithTag("Player");
            foreach (var point in temp)
            {
                point.SetActive(false);
            }
        }

        //结束逻辑
        private void EndGame()
        {
            var temp = _rigidbody2D.velocity;
            temp.x = Mathf.Clamp(temp.x, 0, 0);
            temp.y = 2;
            _rigidbody2D.velocity = temp;
            _light2D.intensity += 0.001f;
            _light2D.pointLightOuterRadius += 0.1f;
        }

        public void Load()
        {
            Init.Init.InitScene(Loader.Scene.Stop);
            SceneManager.LoadSceneAsync("Init", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("Level3", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("Level4", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("Credit", LoadSceneMode.Additive);
        }

        private IEnumerator Next()
        {
            if (scene != Loader.Scene.Credit)
            {
                yield return new WaitForSecondsRealtime(1);
                _player.transform.position = Vector2.zero;
                _rigidbody2D.simulated = false;
                Init.Init.InitScene(scene);
                SceneManager.LoadScene("Init");
            }
            else
            {
                //缓进
                _player.GetComponent<Rigidbody2D>().gravityScale = -1;
                spriteRenderer.color = Color.white;
                _player.GetComponent<Player.Player>().isEnd = true;
                _player.GetComponent<Player.Player>().SaveData();
                yield return new WaitForSeconds(2);
                _bgmOut = true;
                yield return new WaitForSeconds(7);
                _player.gameObject.transform.position = Vector2.zero;
                SilentAll();
                NetworkManager.Singleton.Shutdown();
                SceneManager.LoadScene("Credit");
            }
        }

        private void BgmOut()
        {
            bgm.volume -= 0.005f;
        }

        private void Update()
        {
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.GetComponent<Player.Player>().IsLocalPlayer)
                    {
                        _player = point;
                        _light2D = _player.GetComponentInChildren<Light2D>();
                        _rigidbody2D = _player.GetComponent<Rigidbody2D>();
                        _rigidbody2D.simulated = true;
                        _audioSource = GetComponent<AudioSource>();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void FixedUpdate()
        {
            if (_bgmOut) BgmOut();
            if (_isEnded) EndGame();
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a, TargetAlpha, 0.01f * Time.deltaTime);
            spriteRenderer.color = color;
        }
    }
}