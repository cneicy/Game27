using System.Collections;
using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class Dash : MonoBehaviour
    {
        private float _dashForce = 200f;
        private Rigidbody2D _rigidBody2D;
        private Animator _animator;
        private VAttack _vAttack;
        public bool isDashing;
        private bool _canDash = true;
        private static readonly int Dash1 = Animator.StringToHash("Dash");
        private ShadowPool _shadowPool;
        [SerializeField]private AudioSource audioSource;
        
        private void Awake()
        {
            _shadowPool = GameObject.FindWithTag("Player").GetComponent<ShadowPool>();
            _rigidBody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            _animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
            _vAttack = GameObject.FindWithTag("VAttack").GetComponent<VAttack>();
        }

        private void PlayerDash()
        {
            if (Input.GetKeyDown(KeyCode.L) && !isDashing && _canDash)
            {
                isDashing = true;
                _canDash = false;
                _vAttack.canAttack = true;
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
                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * _dashForce), ForceMode2D.Impulse);
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
            PlayerDash();
        }
    }
}