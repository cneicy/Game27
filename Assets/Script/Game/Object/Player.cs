using System;
using System.Collections;
using Script.Data;
using Script.Game.Getter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game.Object
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public float playerSpeed;
        private Rigidbody2D _rigidBody2D;
        private Quaternion _tempY;
        private int _hp;
        public static PlayerData PlayerData;
        public bool jumping;
        public float gravityScale;
        public float jumpHeight;
        public float fallGravityScale;
        public float buttonPressTime;
        public float buttonPressWindow;
        private bool _canJump;
        private bool _isCoyoteTimeEnable;
        private float _coyoteTime;
        [SerializeField] private float coyoteTimeMax;
        [SerializeField] private AudioSource walkingSource;
        [SerializeField] private AudioSource runningSource;
        [SerializeField] private AudioSource jumpingSource;

        [SerializeField] private float rayStart;
        /*[SerializeField] private BoxCollider2D leftCollider;
        [SerializeField] private BoxCollider2D rightCollider;*/
        private bool _leftWallJump;
        private bool _rightWallJump;
        private EdgeCollider2D _wall;

        
        
        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<EdgeCollider2D>();
        }

        //保存数据用Trigger方法
        public void SaveData()
        {
            //todo 完成玩家阶段储存
            PlayerData.Hp = _hp;
            PlayerData.PlayerTransform = transform;
        }

        private void JumpTime()
        {
            if (_canJump || _leftWallJump || _rightWallJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpingSource.Play();
                    _canJump = false;
                    _isCoyoteTimeEnable = false;
                    if (_leftWallJump)
                    {
                        _rigidBody2D.AddForce(Vector2.right*800f);
                        _rigidBody2D.AddForce(Vector2.up*100);
                        _rigidBody2D.gravityScale = gravityScale;
                    }
                    if (_rightWallJump)
                    {
                        _rigidBody2D.AddForce(Vector2.left*800f);
                        _rigidBody2D.AddForce(Vector2.up*100);
                        _rigidBody2D.gravityScale = gravityScale;
                    }
                    _coyoteTime = 0;
                    _rigidBody2D.gravityScale = gravityScale;
                    var jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * _rigidBody2D.gravityScale) * -2) *
                                    _rigidBody2D.mass;
                    _rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    jumping = true;
                    buttonPressTime = 0; //重置
                }
            }

            if (!jumping) return;
            buttonPressTime += Time.deltaTime; //开始计时

            //在上升过程中松开按键
            if (buttonPressTime < buttonPressWindow && Input.GetKeyUp(KeyCode.Space))
            {
                _rigidBody2D.gravityScale = fallGravityScale;
            }

            //玩家开始下落（物体达到高度峰值），完成了完整跳跃
            if (!(_rigidBody2D.velocity.y < 0)) return;
            jumping = false; //物体开始下落就设置为false
            _rigidBody2D.gravityScale = fallGravityScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Wall"))
            {
                _isCoyoteTimeEnable = false;
                _canJump = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _coyoteTime = 0;
            if (other.gameObject.tag.Equals("Wall"))
            {
                _canJump = true;
            }
        }

        //当玩家离地自动启用土狼时间 允许玩家跳跃一次 土狼时间为10fixUpdate 如果玩家在10fixUpdate内跳跃过则禁用土狼时间
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Wall"))
            {
                _isCoyoteTimeEnable = true;
            }
        }

        private void PlayerMove() //不能直接*KeyGetter.PlayerDir 会起飞
        {
            if (KeyGetter.PlayerDir.x != 0)
            {
                walkingSource.Play();
                runningSource.PlayDelayed(0.5f);
                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * playerSpeed));
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Clamp(temp.x, -7, 7);
                _rigidBody2D.velocity = temp;
            }
            else
            {
                walkingSource.Pause();
                runningSource.Pause();
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Lerp(temp.x, 0f, 0.02f);
                _rigidBody2D.velocity = temp;
            }
        }

        private void CoyoteTime()
        {
            if (_coyoteTime > coyoteTimeMax)
            {
                _canJump = false;
            }
        }

        //玩家转身
        private void Turn()
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
            if (_isCoyoteTimeEnable)
            {
                _coyoteTime += Time.fixedDeltaTime;
            }
        }

        private void ClimbJump()
        {
            var leftTemp = transform.position + Vector3.left * rayStart + Vector3.up * 0.5f;
            
            var leftRay = Physics2D.Raycast(leftTemp, Vector2.left, 0.1f);
            
            var rightTemp = transform.position + Vector3.right * rayStart + Vector3.up * 0.5f;
            
            var rightRay = Physics2D.Raycast(rightTemp, Vector2.right, 0.1f);
            Debug.DrawRay(leftTemp,Vector2.left,Color.cyan,0.1f);
            Debug.DrawRay(rightTemp,Vector2.right,Color.blue,0.1f);
            //Debug.DrawLine(transform.position,);
            if (leftRay.collider is not null)
            {
                if (leftRay.collider.tag.Equals("Wall"))
                {
                    _leftWallJump = true;
                }
            }
            else
            {
                _leftWallJump = false;
            }
            
            if (rightRay.collider is not null)
            {
                if (rightRay.collider.tag.Equals("Wall"))
                {
                    _rightWallJump = true;
                }
            }
            else
            {
                _rightWallJump = false;
            }
        }

        private void Update()
        {
            ClimbJump();
            CoyoteTime();
            PlayerMove();
            Turn();
            JumpTime();
        }
    }
}