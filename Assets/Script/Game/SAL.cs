using System.IO;
using Newtonsoft.Json;
using Script.Data;
using Script.Game.Object.Player;
using UnityEngine;

namespace Script.Game
{
    //Save And Load 游戏保存与加载类
    public class SAL : MonoBehaviour
    {
        public string _json;
        public string _filePath;
        private Player _player;
        public PlayerData _playerData;
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            //检测并创建持久化目录 获取持久化路径
            if (!Directory.Exists(Application.persistentDataPath))
                Directory.CreateDirectory(Application.persistentDataPath);
            _filePath = Application.persistentDataPath + "/" + "PlayerData.json";
            if (!File.Exists(_filePath))
            {
                _playerData.PlayerPosition = Vector3.zero;
                _playerData.Hp = 3;
                _json = JsonUtility.ToJson(_playerData);
                File.WriteAllText(_filePath,_json);
            }
            else
            {
                _json = File.ReadAllText(_filePath);
            }
        }
        //数据保存Trigger方法
        public void Save()
        {
            _json = JsonUtility.ToJson(_player.PlayerData);
            File.WriteAllText(_filePath,_json);
        }
        //数据加载Trigger方法
        public void Load()
        {
            _player.PlayerData = JsonConvert.DeserializeObject<PlayerData>(_json);
        }
    }
}