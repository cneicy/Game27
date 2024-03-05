using UnityEngine;

namespace Script.MainMenu.UI
{
    public class Setting : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        //todo 声音调整 键位调整
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void BackToMenu()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
