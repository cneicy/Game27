using UnityEngine;

namespace Script.Game.Object
{
    public class RedLine : MonoBehaviour
    {
        private SpriteRenderer _player;
        private AudioSource _audioSource;
        [SerializeField] private GameObject[] directors;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        }

        //引导光点复位
        private void OnTriggerEnter2D(Collider2D other)
        {
            _audioSource.Play();
            foreach (var director in directors)
            {
                director.SetActive(true);
            }
            _player.transform.position = Vector2.zero;
        }
    }
}