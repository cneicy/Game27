using TMPro;
using UnityEngine;

namespace Script.Game.UI
{
    public class Notice : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmp;
        private KeySettingManager _keySettingManager;
        private void Awake()
        {
            _keySettingManager = GameObject.FindWithTag("KeySettingManager").GetComponent<KeySettingManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (gameObject.tag)
            {
                case "NoticeWall":
                    tmp.text = "墙跳: " + _keySettingManager.GetKey("Jump");
                    break;
                case "NoticeJump":
                    tmp.text = "跳跃: " + _keySettingManager.GetKey("Jump");
                    break;
                case "NoticeDash":
                    tmp.text = "冲刺: " + _keySettingManager.GetKey("Dash");
                    break;
            }
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                tmp.text = "";
            }
        }
    }
}