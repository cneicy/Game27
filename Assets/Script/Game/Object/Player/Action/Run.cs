using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private AudioSource walkingSource;
        [SerializeField] private AudioSource runningSource;
        [SerializeField] public float playerSpeed;
        private Rigidbody2D _rigidBody2D;
        private Animator _animator;
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        private void Awake()
        {
            _rigidBody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            _animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }

        private void PlayerMove() //不能直接*KeyGetter.PlayerDir 会起飞
        {
            if (KeyGetter.PlayerDir.x != 0)
            {
                _animator.SetBool(IsRunning, true);
                walkingSource.Play();
                runningSource.PlayDelayed(0.5f);
                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * playerSpeed));
            }
            else
            {
                walkingSource.Pause();
                runningSource.Pause();
                _animator.SetBool(IsRunning, false);
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Lerp(temp.x, 0f, 0.4f);
                _rigidBody2D.velocity = temp;
            }
        }

        private void Update()
        {
            PlayerMove();
        }
    }
}