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
            _tips.Add("Hello World!");
        }

        //随机加载tip
        private void Start()
        {
            _tipText.text = _tips[Random.Range(0, _tips.Count)];
        }
    }
}