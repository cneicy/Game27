using UnityEngine;

namespace Script.Data
{
    public struct PlayerData
    //玩家数据结构体，用于转换成json持久化保存
    {
        public int Hp;
        public Transform PlayerTransform;
        public enum Stage
        {
            Level1, Level2, Level3
        }
        public bool IsFinish;
    }
}