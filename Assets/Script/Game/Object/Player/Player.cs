using System.Collections;
using Script.Data;
using Script.Game.Object.Player.Action;
using UnityEngine;

namespace Script.Game.Object.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rigidBody2D;
        private Animator _animator;
        private int _hp;
        public PlayerData PlayerData;
        public bool jumping;
        public float gravityScale;
        public float jumpHeight;
        public float fallGravityScale;
        public float buttonPressTime;
        public float buttonPressWindow;
        public bool canJump;
        private bool _isCoyoteTimeEnable;
        private bool _isPreJumpEnable;
        private float _coyoteTime;
        private float _preJumpTime;
        private Dash _dash;
        private bool _isJumpCooling;
        [SerializeField] private float preJumpTimeMax;
        [SerializeField] private float coyoteTimeMax;

        [SerializeField] private AudioSource jumpingSource;

        [SerializeField] private float rayStart;

        /*[SerializeField] private BoxCollider2D leftCollider;
        [SerializeField] private BoxCollider2D rightCollider;*/
        public bool leftWallJump;
        public bool rightWallJump;
        private static readonly int Jump1 = Animator.StringToHash("isJumping");
        private KeySettingManager _keySettingManager;

        private SAL _sal;
        
        private void Awake()
        {
            _sal = GameObject.FindWithTag("Global").GetComponent<SAL>();
            _sal.InitPlayer();
            _keySettingManager = GameObject.FindWithTag("KeySettingManager").GetComponent<KeySettingManager>();
            _dash = GetComponent<Dash>();
            _animator = GetComponent<Animator>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _rigidBody2D.freezeRotation = true;
        }

        private void Start()
        {
            LoadData();
        }
        
        //保存数据用Trigger方法
        public void SaveData()
        {
            //todo 完成玩家阶段储存
            PlayerData.Hp = _hp;
            PlayerData.PlayerPosition = transform.position;
            _sal.Save();
        }

        public void LoadData()
        {
            _sal.Load();
            gameObject.transform.position = PlayerData.PlayerPosition;
            _hp = PlayerData.Hp;
        }
        private IEnumerator JumpCoolDown()
        {
            yield return new WaitForSeconds(0.2f);
            _isJumpCooling = false;
        }
        private void JumpTime()
        {
            if (!canJump && Input.GetKeyDown(_keySettingManager.GetKey("Jump")) && !_isPreJumpEnable && !_isJumpCooling)
            {
                _isJumpCooling = true;
                StartCoroutine(JumpCoolDown());
                _isPreJumpEnable = true;
            }

            if (!_isJumpCooling && (canJump || leftWallJump || rightWallJump))
            {
                if (Input.GetKeyDown(_keySettingManager.GetKey("Jump")))
                {
                    _isJumpCooling = true;
                    StartCoroutine(JumpCoolDown());
                    Jump();
                }
            }

            if (!jumping) return;
            buttonPressTime += Time.deltaTime; //开始计时

            //在上升过程中松开按键
            if (buttonPressTime < buttonPressWindow && Input.GetKeyUp(_keySettingManager.GetKey("Jump")))
            {
                _rigidBody2D.gravityScale = fallGravityScale;
            }

            //玩家开始下落（达到高度峰值），完成了完整跳跃
            if (!(_rigidBody2D.velocity.y < 0)) return;
            
            jumping = false; //物体开始下落就设置为false
            _rigidBody2D.gravityScale = fallGravityScale;
        }

        private IEnumerator JumpAnimationTimer()
        {
            _animator.SetBool(Jump1,true);
            yield return new WaitForSeconds(1f);
            _animator.SetBool(Jump1,false);
        }
        
        private void Jump()
        {
            jumpingSource.Play();
            canJump = false;
            _isCoyoteTimeEnable = false;
            var jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * _rigidBody2D.gravityScale) * -2) *
                            _rigidBody2D.mass;
            if (leftWallJump)
            {
                _rigidBody2D.AddForce(Vector2.right * 2000f);
                _rigidBody2D.AddForce(Vector2.up * jumpForce / 1.5f);
                _rigidBody2D.gravityScale = gravityScale;
            }

            if (rightWallJump)
            {
                _rigidBody2D.AddForce(Vector2.left * 2000);
                _rigidBody2D.AddForce(Vector2.up * jumpForce / 1.5f);
                _rigidBody2D.gravityScale = gravityScale;
            }

            _coyoteTime = 0;
            _rigidBody2D.gravityScale = gravityScale;
            StartCoroutine(JumpAnimationTimer());
            _rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            buttonPressTime = 0; //重置
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.gameObject.tag.Equals("Wall"))
            {
                if (_preJumpTime < preJumpTimeMax && _isPreJumpEnable && !leftWallJump && !rightWallJump)
                {
                    Jump();
                }

                _isCoyoteTimeEnable = false;
                _isPreJumpEnable = false;
                canJump = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _coyoteTime = 0;
            _preJumpTime = 0;
            if (other.gameObject.tag.Equals("Wall"))
            {
                canJump = true;
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
            temp.x = _dash.isDashing ? Mathf.Clamp(temp.x, -50, 50) : Mathf.Clamp(temp.x, -5.5f, 5.5f);
            _rigidBody2D.velocity = temp;
        }

        private void CoyoteTime()
        {
            if (_coyoteTime > coyoteTimeMax)
            {
                canJump = false;
            }
        }
        

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
                    leftWallJump = true;
                }
            }
            else
            {
                leftWallJump = false;
            }

            if (rightRay.collider is not null)
            {
                if (rightRay.collider.tag.Equals("Wall"))
                {
                    rightWallJump = true;
                }
            }
            else
            {
                rightWallJump = false;
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