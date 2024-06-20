using System;
using System.Collections;
using System.Threading.Tasks;
using Script.Init;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Game.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject normalPanel;
        [SerializeField] private GameObject savingText;
        private bool loadTrigger;
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

            if (loadTrigger)
            {
                loadTrigger = false;
                StartCoroutine(StopCamera());
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

        public async void StartServer()
        {
            NetworkManager.Singleton.Shutdown();
            await Task.Delay(1);
            NetworkManager.Singleton.StartServer();
            loadTrigger = true;
        }

        private IEnumerator StopCamera()
        {
            yield return new WaitForSeconds(1);
            try
            {
                GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
            }
            catch
            {
                // ignored
            }
            
            StartCoroutine(StopCamera());
        }
    }
}