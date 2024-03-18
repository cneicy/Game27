using UnityEngine;

namespace Script.Game.UI
{
    public class Mask : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private const int TargetAlpha = 0;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void FixedUpdate()
        {
            if(_spriteRenderer.color.a<0) return;
            var color = _spriteRenderer.color;
            color.a = Mathf.Lerp(color.a,TargetAlpha, 0.1f * Time.deltaTime);
            
            _spriteRenderer.color = color;
        }
    }
}