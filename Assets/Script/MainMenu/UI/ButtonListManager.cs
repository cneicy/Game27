using System.IO;
using UnityEngine;

namespace Script.MainMenu.UI
{
    public class ButtonListManager : MonoBehaviour
    {
        [SerializeField] private GameObject list1;

        [SerializeField] private GameObject list2;
        private void Start()
        {
            if (File.Exists(Application.persistentDataPath + "/" + "PlayerData.json"))
            {
                list1.SetActive(false);
                list2.SetActive(true);
            }
            else
            {
                list1.SetActive(true);
                list2.SetActive(false);
            }
        }
    }
}