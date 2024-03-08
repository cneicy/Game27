using Script.Data;
using Script.Game.Object.Player.Action;
using UnityEngine;

namespace Script.Game.Object.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rigidBody2D;

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
        private bool _isPreJumpEnable;
        private float _coyoteTime;
        private float _preJumpTime;
        private Dash _dash;
        [SerializeField] private float preJumpTimeMax;
        [SerializeField] private float coyoteTimeMax;

        [SerializeField] private AudioSource jumpingSource;

        [SerializeField] private float rayStart;

        /*[SerializeField] private BoxCollider2D leftCollider;
        [SerializeField] private BoxCollider2D rightCollider;*/
        private bool _leftWallJump;
        private bool _rightWallJump;


        private void Awake()
        {
            _dash = GetComponent<Dash>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _rigidBody2D.freezeRotation = true;
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
            if (!_canJump && Input.GetKeyDown(KeyCode.K) && !_isPreJumpEnable)
            {
                _isPreJumpEnable = true;
            }

            if (_canJump || _leftWallJump || _rightWallJump)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Jump();
                }
            }

            if (!jumping) return;
            buttonPressTime += Time.deltaTime; //开始计时

            //在上升过程中松开按键
            if (buttonPressTime < buttonPressWindow && Input.GetKeyUp(KeyCode.K))
            {
                _rigidBody2D.gravityScale = fallGravityScale;
            }

            //玩家开始下落（物体达到高度峰值），完成了完整跳跃
            if (!(_rigidBody2D.velocity.y < 0)) return;
            jumping = false; //物体开始下落就设置为false
            _rigidBody2D.gravityScale = fallGravityScale;
        }

        private void Jump()
        {
            jumpingSource.Play();
            _canJump = false;
            _isCoyoteTimeEnable = false;
            var jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * _rigidBody2D.gravityScale) * -2) *
                            _rigidBody2D.mass;
            if (_leftWallJump)
            {
                _rigidBody2D.AddForce(Vector2.right * 1200f);
                _rigidBody2D.AddForce(Vector2.up * jumpForce / 1.5f);
                _rigidBody2D.gravityScale = gravityScale;
            }

            if (_rightWallJump)
            {
                _rigidBody2D.AddForce(Vector2.left * 1200);
                _rigidBody2D.AddForce(Vector2.up * jumpForce / 1.5f);
                _rigidBody2D.gravityScale = gravityScale;
            }

            _coyoteTime = 0;
            _rigidBody2D.gravityScale = gravityScale;

            _rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            buttonPressTime = 0; //重置
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Wall"))
            {
                if (_preJumpTime < preJumpTimeMax && _isPreJumpEnable && !_leftWallJump && !_rightWallJump)
                {
                    Jump();
                }

                _isCoyoteTimeEnable = false;
                _isPreJumpEnable = false;
                _canJump = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _coyoteTime = 0;
            _preJumpTime = 0;
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


        private void SpeedLimit()
        {
            var temp = _rigidBody2D.velocity;
            //玩家冲刺限速
            if (_dash.isDashing)
            {
                temp.x = Mathf.Clamp(temp.x, -50, 50);
            }
            else
            {
                temp.x = Mathf.Clamp(temp.x, -7, 7);
            }

            _rigidBody2D.velocity = temp;
        }

        private void CoyoteTime()
        {
            if (_coyoteTime > coyoteTimeMax)
            {
                _canJump = false;
            }
        }

        //玩家转身


        private void FixedUpdate()
        {
            if (_isCoyoteTimeEnable)
            {
                _coyoteTime += Time.fixedDeltaTime;
            }

            if (_isPreJumpEnable)
            {
                _preJumpTime += Time.fixedDeltaTime;
            }
        }

        private void ClimbJump()
        {
            var leftTemp = transform.position + Vector3.left * rayStart + Vector3.up * 0.3f;

            var leftRay = Physics2D.Raycast(leftTemp, Vector2.left, 0.1f);

            var rightTemp = transform.position + Vector3.right * rayStart + Vector3.up * 0.3f;

            var rightRay = Physics2D.Raycast(rightTemp, Vector2.right, 0.1f);
            Debug.DrawRay(leftTemp, Vector2.left, Color.cyan, 0.1f);
            Debug.DrawRay(rightTemp, Vector2.right, Color.blue, 0.1f);

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
            SpeedLimit();
            ClimbJump();
            CoyoteTime();
            JumpTime();
        }
    }
}