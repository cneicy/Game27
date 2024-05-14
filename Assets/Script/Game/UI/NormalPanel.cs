using Script.Game.Object.Player;
using UnityEngine;

namespace Script.Game.UI
{
    public class NormalPanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private Player _player;
        

        public void Pause()
        {
            if(_player)
                _player.SaveData();
            
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("Player");
                foreach (var point in temp)
                {
                    if (point.GetComponent<Player>().IsLocalPlayer)
                    {
                        _player = point.GetComponent<Player>();
                    }
                }
            }
            catch
            {
            }
            
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
}