using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Init.UI
{
    public class Tip : MonoBehaviour
    {
        //todo 加载场景Tip
        private List<string> _tips = new();
        private TMP_Text _tipText;
        private void Awake()
        {
            _tipText = GetComponent<TMP_Text>();
            _tips.Add("在墙边一直按跳跃可以进行超级跳！");
            _tips.Add("也许这款游戏在手感上花费了很多开发时间。");
        }

        //随机加载tip
        private void Start()
        {
            Roll();
        }

        public void Roll()
        {
            _tipText.text = "Tips: "+_tips[Random.Range(0, _tips.Count)];
        }
    }
}