using System.IO;
using Script.Data;
using Script.Game.Object.Player;
using UnityEngine;

namespace Script.Game
{
    //Save And Load 游戏保存与加载类
    public class SAL : MonoBehaviour
    {
        private string _json;
        private string _filePath;

        private void Awake()
        {
            //检测并创建持久化目录 获取持久化路径
            if (!Directory.Exists(Application.persistentDataPath))
                Directory.CreateDirectory(Application.persistentDataPath);
            _filePath = Application.persistentDataPath + "/" + "Data.json";
        }
        //数据保存Trigger方法
        public void Save()
        {
            _json = JsonUtility.ToJson(Player.PlayerData);
            File.WriteAllText(_filePath,_json);
        }
        //数据加载Trigger方法
        public void Load()
        {
            Player.PlayerData = JsonUtility.FromJson<PlayerData>(_json);
            
        }
    }
}