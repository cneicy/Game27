using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.Credit.UI
{
    public class ButtonText : MonoBehaviour
    {
        public TMP_Text textMeshPro;
        private const string IntroduceText = "返回主菜单 ";
        public float typingSpeed = 0.05f;

        private string _currentText = "";
        private float _timer;
        private int _currentIndex;
        private bool _start;
        private bool _isBlinking;
        private int _blinkTime;

        private void Start()
        {
            textMeshPro.text = "";
            _currentText = "";
            _currentIndex = 0;
            StartCoroutine(StartPrinting());
        }

        private void Update()
        {
            if (!_start) return;
            if (_currentText == IntroduceText) return;
            _timer += Time.deltaTime;
            if (!(_timer >= typingSpeed)) return;
            _timer = 0f;
            _currentText += IntroduceText[_currentIndex];
            textMeshPro.text = _currentText;
            _currentIndex++;
        }

        //闪烁光标
        private IEnumerator BlinkCursor()
        {
            while (true)
            {
                if (_blinkTime++ > 10)
                {
                    textMeshPro.text = textMeshPro.text[..^1];
                    yield break;
                }

                if (!_isBlinking)
                {
                    textMeshPro.text += "|";
                    _isBlinking = true;
                }
                else
                {
                    textMeshPro.text = textMeshPro.text[..^1];
                    _isBlinking = false;
                }

                yield return new WaitForSeconds(0.3f); // 闪烁速度
            }
        }

        // 调用此方法开始打印文字
        private IEnumerator StartPrinting()
        {
            yield return new WaitForSeconds(5);
            _start = true;
            textMeshPro.text = "";
            _currentText = "";
            _currentIndex = 0;
            _timer = 0f;
            StartCoroutine(BlinkCursor());
        }
    }
}