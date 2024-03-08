using System.Collections;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class VAttack : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidBody2D;
        private Flip _playerFlip;
        public bool canAttack = true;

        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        //private PolygonCollider2D _polygonCollider2D;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerFlip = GameObject.FindWithTag("Player").GetComponent<Flip>();
            _rigidBody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            //_polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        private void LockPlayer()
        {
            if (canAttack) return;
            _rigidBody2D.velocity = Vector2.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J) && canAttack)
            {
                canAttack = false;
                _animator.SetBool(IsAttacking,true);
                _playerFlip.canFlip = false;
                //_polygonCollider2D.enabled = true;
                StartCoroutine(AttackCoolDown());
            }

            LockPlayer();
        }

        private IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(0.45f);
            //_polygonCollider2D.enabled = false;
            _animator.SetBool(IsAttacking,false);
            _playerFlip.canFlip = true;
            canAttack = true;
        }
    }
}