using UnityEngine;

namespace Script.Game.Getter
{
    public class KeyGetter : MonoBehaviour
    {
        private KeySettingManager _keySettingManager;
        private Vector2 _playerDir;
        private void Start()
        {
            _keySettingManager = GameObject.FindWithTag("KeySettingManager").GetComponent<KeySettingManager>();
        }
        /*private Controller _controller;

        private void Awake()
        {
            _controller = new();
        }

        private void OnEnable()
        {
            _controller.Enable();
        }

        private void OnDisable()
        {
            _controller.Disable();
        }*/

        public static Vector2 PlayerDir { get;private set; }
        /*private void FixedUpdate()
        {
            PlayerDir = _controller.Game.Player.ReadValue<Vector2>();
        }*/
        private void Update()
        {
            if ((Input.GetKey(_keySettingManager.GetKey("Left")) && Input.GetKey(_keySettingManager.GetKey("Right"))) || !(Input.GetKey(_keySettingManager.GetKey("Left")) && !Input.GetKey(_keySettingManager.GetKey("Right"))))
            {
                _playerDir.x = 0;
            }
            if (Input.GetKey(_keySettingManager.GetKey("Left")) && !Input.GetKey(_keySettingManager.GetKey("Right")))
            {
                _playerDir.x = -1;
            }

            if (!Input.GetKey(_keySettingManager.GetKey("Left")) && Input.GetKey(_keySettingManager.GetKey("Right")))
            {
                _playerDir.x = 1;
            }

            PlayerDir = _playerDir;
        }
    }
}