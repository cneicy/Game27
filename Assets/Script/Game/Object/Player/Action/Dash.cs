using System.Collections;
using Script.Game.Getter;
using Unity.Netcode;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class Dash : NetworkBehaviour
    {
        private const float DashForce = 200f;
        private Rigidbody2D _rigidBody2D;
        private Animator _animator;

        private GameObject _player;

        //private VAttack _vAttack;
        public bool isDashing;
        private bool _canDash = true;
        private static readonly int Dash1 = Animator.StringToHash("Dash");
        private ShadowPool _shadowPool;
        private KeySettingManager _keySettingManager;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            _keySettingManager = GameObject.FindWithTag("KeySettingManager").GetComponent<KeySettingManager>();

            //_vAttack = GameObject.FindWithTag("VAttack").GetComponent<VAttack>();
        }

        private void PlayerDash()
        {
            if (Input.GetKeyDown(_keySettingManager.GetKey("Dash")) && !isDashing && _canDash)
            {
                isDashing = true;
                _canDash = false;
                //_vAttack.canAttack = true;
                _animator.SetTrigger(Dash1);
                audioSource.Play();
                //取消跳跃力
                var temp = _rigidBody2D.velocity;
                temp.y = 0;
                _rigidBody2D.velocity = temp;
                var gTemp = _rigidBody2D.gravityScale;
                StartCoroutine(DashTimer(gTemp));
                StartCoroutine(DashCoolDown());
                _rigidBody2D.gravityScale = 0;
                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * DashForce), ForceMode2D.Impulse);
            }

            //冲刺残影
            if (isDashing)
            {
                _shadowPool.Spawn(gameObject);
            }
        }

        private IEnumerator DashTimer(float gravityScale)
        {
            yield return new WaitForSeconds(0.1f);
            _rigidBody2D.gravityScale = gravityScale;
            isDashing = false;
        }

        private IEnumerator DashCoolDown()
        {
            yield return new WaitForSeconds(0.6f);
            _canDash = true;
        }

        private void Update()
        {
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.GetComponent<Player>().IsLocalPlayer)
                    {
                        _player = point;
                        _shadowPool = _player.GetComponent<ShadowPool>();
                        _rigidBody2D = _player.GetComponent<Rigidbody2D>();
                        _animator = _player.GetComponent<Animator>();
                    }
                }
            }
            catch
            {
            }

            if (IsOwner)
                PlayerDash();
        }
    }
}