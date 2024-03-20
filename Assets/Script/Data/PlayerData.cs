using Script.Init;
using UnityEngine;

namespace Script.Data
{
    public struct PlayerData
        //玩家数据结构体，用于转换成json持久化保存
    {
        public int Hp;
        public Vector3 PlayerPosition;
        public Loader.Scene Scene;
        public bool IsFinish;//游戏是否结束
    }
}