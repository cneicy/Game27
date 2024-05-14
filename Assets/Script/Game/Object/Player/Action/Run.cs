﻿using Script.Game.Getter;
using Unity.Netcode;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class Run : NetworkBehaviour
    {
        [SerializeField] private AudioSource walkingSource;
        [SerializeField] private AudioSource runningSource;
        [SerializeField] public float playerSpeed;
        private Rigidbody2D _rigidBody2D;
        private Animator _animator;
        private Player _player;
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        private void Awake()
        {
        }

        private void PlayerMove() //不能直接*KeyGetter.PlayerDir 会起飞
        {
            if (KeyGetter.PlayerDir.x != 0)
            {
                _animator.SetBool(IsRunning, true);

                if (!walkingSource.isPlaying && !runningSource.isPlaying && !_player.leftWallJump &&
                    !_player.rightWallJump && !_player.jumping && _player.canJump)
                {
                    var temp = Random.Range(1, 3);
                    switch (temp)
                    {
                        case 1:
                            walkingSource.Play();
                            break;
                        case 2:
                            runningSource.Play();
                            break;
                        default:
                            walkingSource.Play();
                            break;
                    }
                }

                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * playerSpeed));
            }
            else
            {
                _animator.SetBool(IsRunning, false);
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Lerp(temp.x, 0f, 0.4f);
                _rigidBody2D.velocity = temp;
            }
        }

        private void Update()
        {
            if (IsOwner)
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.gameObject.GetComponentInParent<Player>().OwnerClientId == NetworkObject.OwnerClientId)
                    {
                        _player = point.GetComponent<Player>();
                        _rigidBody2D = point.GetComponent<Rigidbody2D>();
                        _animator = point.GetComponent<Animator>();
                    }
                }

                PlayerMove();
            }
        }
    }
}