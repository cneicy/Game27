using System.Collections;
using Script.Init;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Credit
{
    public class Global : MonoBehaviour
    {
        /// <summary>
        /// 返回主菜单 控制bgm缓进 
        /// </summary>
        [SerializeField] private GameObject btn;

        private AudioSource _bgm;
        private float _vo;

        private void Awake()
        {
            _bgm = GetComponent<AudioSource>();
            _vo = _bgm.volume;
            _bgm.volume = 0;
            btn.SetActive(false);
        }

        private void Start()
        {
            StartCoroutine(ShowButton());
        }

        private void FixedUpdate()
        {
            if (_bgm.volume < _vo)
            {
                _bgm.volume += 0.005f;
            }
        }

        private IEnumerator ShowButton()
        {
            yield return new WaitForSeconds(40);
            btn.SetActive(true);
        }

        public void Back2Menu()
        {
            Init.Init.InitScene(Loader.Scene.MainMenu);
            SceneManager.LoadScene("Init");
        }
    }
}