using Script.Data;
using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public float playerSpeed = 600f;
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

        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
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
            if (_canJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _canJump = false;
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
            if (_rigidBody2D.velocity.y < 0)
            {
                jumping = false; //物体开始下落就设置为false
                _rigidBody2D.gravityScale = fallGravityScale;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _canJump = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _canJump = true;
        }

        private void Update()
        {
            //不能直接*KeyGetter.PlayerDir 会起飞
            if (KeyGetter.PlayerDir.x != 0)
            {
                _rigidBody2D.AddForce(Vector2.right * (KeyGetter.PlayerDir.x * playerSpeed));
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Clamp(temp.x, -4, 4);
                _rigidBody2D.velocity = temp;
            }
            else
            {
                var temp = _rigidBody2D.velocity;
                temp.x = Mathf.Lerp(temp.x, 0f, 0.02f);
                _rigidBody2D.velocity = temp;
            }
            //玩家转身

            _tempY.y = KeyGetter.PlayerDir.x switch
            {
                < 0 => 180,
                > 0 => 0,
                _ => _tempY.y
            };
            transform.rotation = _tempY;
            JumpTime();
        }
    }
}