using System.Collections;
using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object.Player
{
    public class Shadow : MonoBehaviour
    {
        private ShadowPool _shadowPool;
        private Quaternion _tempY;
        private void Awake()
        {
            _shadowPool = GameObject.FindWithTag("Player").GetComponent<ShadowPool>();
        }

        private void Start()
        {
            _tempY.y = KeyGetter.PlayerDir.x switch
            {
                < 0 => 180,
                > 0 => 0,
                _ => _tempY.y
            };
            transform.rotation = _tempY;
        }

        private void FixedUpdate()
        {
            StartCoroutine(Shrink());
        }

        private IEnumerator Shrink()
        {
            yield return new WaitForSeconds(0.1f);
            _shadowPool.Release(gameObject);
        }
    }
}