using System.Collections;
using UnityEngine;

namespace Script.Game.Object.Player
{
    public class Shadow : MonoBehaviour
    {
        private ShadowPool _shadowPool;
        private void Awake()
        {
            _shadowPool = GameObject.FindWithTag("Player").GetComponent<ShadowPool>();
            
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