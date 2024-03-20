using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Script
{
    public class KeySettingManager : MonoBehaviour
    {
        public List<KeyMapping> keyMappings = new();
        public string filePath;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            filePath = Application.persistentDataPath + "/" + "keySetting.json";
            LoadKeySettings();
        }

        //加载键位设置
        private void LoadKeySettings()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                keyMappings = JsonConvert.DeserializeObject<List<KeyMapping>>(json);
            }
            else
            {
                //如果文件不存在，则创建默认键位设置
                keyMappings.Add(new KeyMapping("Jump", KeyCode.K));
                keyMappings.Add(new KeyMapping("Attack", KeyCode.J));
                keyMappings.Add(new KeyMapping("Dash", KeyCode.L));
                keyMappings.Add(new KeyMapping("Left", KeyCode.A));
                keyMappings.Add(new KeyMapping("Right", KeyCode.D));
                keyMappings.Add(new KeyMapping("Up", KeyCode.W));
                keyMappings.Add(new KeyMapping("Down", KeyCode.S));
                SaveKeySettings();
            }
        }

        //保存键位设置
        private void SaveKeySettings()
        {
            var json = JsonConvert.SerializeObject(keyMappings, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        //获取键位
        public KeyCode GetKey(string actionName)
        {
            return (from mapping in keyMappings where mapping.actionName == actionName select mapping.keyCode)
                .FirstOrDefault();
        }

        //设置键位
        public void SetKey(string actionName, KeyCode newKeyCode)
        {
            foreach (var mapping in keyMappings.Where(mapping => mapping.actionName == actionName))
            {
                mapping.keyCode = newKeyCode;
                break;
            }

            SaveKeySettings();
        }
    }
}