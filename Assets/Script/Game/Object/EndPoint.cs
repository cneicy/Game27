using System.Collections;
using Script.Init;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Script.Game.Object
{
    public class EndPoint : MonoBehaviour
    {
        private const int TargetAlpha = 255;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Loader.Scene scene;
        [SerializeField] private AudioSource bgm;
        
        private AudioSource _audioSource;
        private GameObject _player;
        private Rigidbody2D _rigidbody2D;
        private Light2D _light2D;
        
        private bool _bgmOut;
        private bool _loadTrigger;
        private bool _isEnded;
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
            //检查是否为第四关结束 如果为第四关结束则执行结束逻辑 如果不是则执行下一关加载
            if (Init.Init.Scene != Loader.Scene.Level4)
            {
                _loadTrigger = true;
                StartCoroutine(Next());
            }
            else
            {
                _isEnded = true;
                StartCoroutine(Next());
            }
        }
        
        //结束逻辑
        private void EndGame()
        {
            var temp = _rigidbody2D.velocity;
            temp.x = Mathf.Clamp(temp.x, 0, 0);
            temp.y = 2;
            _rigidbody2D.velocity = temp;
            _light2D.intensity+=0.001f;
            _light2D.pointLightOuterRadius+=0.1f;
        }
        private IEnumerator Next()
        {
            if(scene!=Loader.Scene.Credit)
            {
                yield return new WaitForSecondsRealtime(1);
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
                SceneManager.LoadScene("Credit");
            }
        }

        private void BgmOut()
        {
            bgm.volume-=0.005f;
        }
        private void FixedUpdate()
        {
            if(_bgmOut) BgmOut();
            if(_isEnded) EndGame();
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            spriteRenderer.color = color;
        }
    }
}