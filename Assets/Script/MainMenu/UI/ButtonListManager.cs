using System.IO;
using Script.Game;
using UnityEngine;

namespace Script.MainMenu.UI
{
    public class ButtonListManager : MonoBehaviour
    {
        [SerializeField] private GameObject list1;
        [SerializeField] private GameObject list2;
        
        private SAL _sal;

        private void Awake()
        {
            _sal = GameObject.FindWithTag("Global").GetComponent<SAL>();
        }
        

        private void FixedUpdate()
        {
            if (File.Exists(Application.persistentDataPath + "/" + "PlayerData.json") && !_sal.PlayerData.IsFinish)
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