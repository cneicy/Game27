using Script.Data;
using Script.Game.Getter;
using UnityEngine;

namespace Script.Game.Object
{
    public class Player : MonoBehaviour
    {
        public static float PlayerSpeed = 2f;
        private Rigidbody2D _rigidbody2D;
        private Quaternion _tempY;
        private int _hp;
        public static PlayerData PlayerData;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        //保存数据用Trigger方法
        public void SaveData()
        {
            PlayerData.Hp = _hp;
            PlayerData.PlayerTransform = transform;
            //todo 完成玩家阶段储存
        }
        
        private void Update()
        {
            //玩家旋转
            _rigidbody2D.velocity = KeyGetter.PlayerDir * PlayerSpeed;
            _tempY.y = KeyGetter.PlayerDir.x switch
            {
                < 0 => 180,
                > 0 => 0,
                _ => _tempY.y
            };
            transform.rotation = _tempY;
        }
    }
}