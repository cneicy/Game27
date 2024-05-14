using System.Collections;
using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object.Player
{
    public class Shadow : MonoBehaviour
    {
        private ShadowPool _shadowPool;
        private Quaternion _tempY;


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
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.GetComponent<Player>().IsLocalPlayer)
                    {
                        _shadowPool = point.GetComponent<ShadowPool>();
                    }
                }
            }
            catch
            {
            }

            StartCoroutine(Shrink());
        }

        private IEnumerator Shrink()
        {
            yield return new WaitForSeconds(0.1f);
            _shadowPool.Release(gameObject);
        }
    }
}