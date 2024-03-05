using Control;
using UnityEngine;

namespace Script.Game.Getter
{
    public class KeyGetter : MonoBehaviour
    {
        private Controller _controller;

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
        }

        public static Vector2 PlayerDir { get;private set; }
        private void FixedUpdate()
        {
            PlayerDir = _controller.Game.Player.ReadValue<Vector2>();
        }
    }
}