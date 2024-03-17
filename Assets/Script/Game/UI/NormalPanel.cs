using System;
using Script.Game.Object.Player;
using UnityEngine;

namespace Script.Game.UI
{
    public class NormalPanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private Player _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

        public void Pause()
        {
            _player.SaveData();
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
}