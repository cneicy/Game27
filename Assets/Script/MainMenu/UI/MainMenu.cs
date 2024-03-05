using System.Collections;
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

        public void StartGame()
        {
            StartCoroutine(Load());
            _loadTrigger = true;
        }
        //延迟一秒加载场景
        private IEnumerator Load()
        {
            yield return new WaitForSeconds(1);
            //选关 以及加载玩家数据位置 传入中间页
            Init.Init.InitScene(Loader.Scene.Level1);
            SceneManager.LoadScene("Init");
        }
        
        private void FixedUpdate()
        {
            if (!_loadTrigger || TargetAlpha == (int)spriteRenderer.color.a) return;
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.01f * Time.deltaTime);
            
            spriteRenderer.color = color;
        }
        
        
        public void OpenSettingPanel()
        {
            settingPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}