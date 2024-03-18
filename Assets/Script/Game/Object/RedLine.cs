using System.Collections;
using UnityEngine;

namespace Script.Game.Object
{
    public class RedLine : MonoBehaviour
    {
        private SpriteRenderer _player;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _player.transform.position = Vector2.zero;
            StartCoroutine(Hit());
        }

        private IEnumerator Hit()
        {
            var temp = _player;
            _player.color = Color.red;
            yield return new WaitForSeconds(0.7f);
            _player.color = temp.color;
        }
    }
}