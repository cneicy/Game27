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
        private static string _json;
        private static string _filePath;
        private Player _player;
        public  PlayerData PlayerData;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            //检测并创建持久化目录 获取持久化路径
            if (!Directory.Exists(Application.persistentDataPath))
                Directory.CreateDirectory(Application.persistentDataPath);
            _filePath = Application.persistentDataPath + "/" + "PlayerData.json";
            if (File.Exists(_filePath))
            {
                InitLoad();
            }
        }

        public void InitLoad()
        {
            _json = File.ReadAllText(_filePath);
            PlayerData = JsonConvert.DeserializeObject<PlayerData>(_json);
        }
        
        public void InitPlayer()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        //数据保存Trigger方法
        public void NewGame()
        {
            PlayerData.PlayerPosition = Vector3.zero;
            PlayerData.Hp = 3;
            PlayerData.Scene = 0;
            _json = JsonUtility.ToJson(PlayerData);
            File.WriteAllText(_filePath,_json);
        }
        public void Save()
        {
            _player.PlayerData.Scene = Init.Init.Scene;
            _json =  JsonUtility.ToJson(_player.PlayerData);
            File.WriteAllText(_filePath,_json);
        }
        //数据加载Trigger方法
        public void Load()
        {
            _player.PlayerData = JsonConvert.DeserializeObject<PlayerData>(_json);
        }
    }
}