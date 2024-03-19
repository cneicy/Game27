using System.Collections;
using Script.Game;
using Script.Init;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.MainMenu.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private const int TargetAlpha = 255;
        //一个trigger 如果玩家点击了开始游戏则设为true并播放加载动画 
        private bool _loadTrigger;
        private SAL _sal;

        private void Awake()
        {
            _sal = GameObject.FindWithTag("Global").GetComponent<SAL>();
        }

        public void StartGame()
        {
            _sal.InitLoad();
            StartCoroutine(Load(_sal.PlayerData.Scene));
            _loadTrigger = true;
        }

        public void NewGame()
        {
            _sal.NewGame();
            StartCoroutine(Load(Loader.Scene.Level1));
            _loadTrigger = true;
        }
        
        //延迟一秒加载场景
        private IEnumerator Load(Loader.Scene scene)
        {
            yield return new WaitForSeconds(1);
            //选关 以及加载玩家数据位置 传入中间页
            Init.Init.InitScene(scene);
            SceneManager.LoadScene("Init");
        }
        
        private void FixedUpdate()
        {
            //渐变遮罩效果
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            
            spriteRenderer.color = color;
        }
        
        //打开设置面板
        public void OpenSettingPanel()
        {
            settingPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        //不 用 注 释
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}