using System.Collections;
using Script.Game.Object.Enemy;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class VAttack : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidBody2D;
        private Flip _playerFlip;
        public bool canAttack = true;
        private AudioSource _audioSource;
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        //private PolygonCollider2D _polygonCollider2D;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _playerFlip = GameObject.FindWithTag("Player").GetComponent<Flip>();
            _rigidBody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            //_polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        private void LockPlayer()
        {
            _rigidBody2D.velocity = Vector2.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J) && canAttack)
            {
                _animator.SetBool(IsAttacking,true);
                _audioSource.Play();
                canAttack = false;
                _playerFlip.canFlip = false;
                LockPlayer();
                //_polygonCollider2D.enabled = true;
                StartCoroutine(AttackCoolDown());
            }
        }
        
        private IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(0.7f);
            //_polygonCollider2D.enabled = false;
            _animator.SetBool(IsAttacking,false);
            _playerFlip.canFlip = true;
            canAttack = true;
            StopAllCoroutines();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Enemy"))
            {
                print(other.gameObject.GetComponent<Noob>().OnDamage(1));
            }
        }
    }
}