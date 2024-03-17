using System;
using System.Collections;
using Script.Init;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject normalPanel;
        [SerializeField] private GameObject savingText;
        private void Start()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            savingText.SetActive(true);
            StartCoroutine(Saving());
        }

        private IEnumerator Saving()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            savingText.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Continue();
            }
        }

        public void Continue()
        {
            normalPanel.SetActive(true);
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            Init.Init.InitScene(Loader.Scene.MainMenu);
            SceneManager.LoadScene("Init");
        }
    }
}