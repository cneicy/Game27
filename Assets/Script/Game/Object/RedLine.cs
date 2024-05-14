using Unity.Netcode;
using UnityEngine;

namespace Script.Game.Object
{
    public class RedLine : NetworkBehaviour
    {
        private SpriteRenderer _player;
        private AudioSource _audioSource;
        [SerializeField] private GameObject[] directors;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.GetComponent<Player.Player>().IsLocalPlayer)
                    {
                        _player = point.GetComponent<SpriteRenderer>();
                    }
                }
            }
            catch
            {
            }
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