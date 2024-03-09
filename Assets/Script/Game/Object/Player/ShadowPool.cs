using UnityEngine;
using UnityEngine.Pool;

namespace Script.Game.Object.Player
{
    public class ShadowPool : MonoBehaviour
    {
        private ObjectPool<GameObject> _pool;
        [SerializeField]private GameObject shadow;

        private void Awake()
        {
            Init(shadow);
        }

        public void Init(GameObject shadowArg)
        {
            shadow = shadowArg;
            _pool = new ObjectPool<GameObject>(CreateFunc,ActionOnGet,ActionOnRelease,ActionOnDestroy,true,1,4);
        }

        private GameObject CreateFunc()
        {
            return Instantiate(shadow);
        }

        private void ActionOnGet(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void ActionOnRelease(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void ActionOnDestroy(GameObject obj)
        {
            Destroy(obj);
        }

        public void Release(GameObject obj)
        {
            _pool.Release(obj);
        }
        public GameObject Spawn(GameObject parent)
        {
            var temp = _pool.Get();
            temp.transform.position = parent.transform.position;
            return temp;
        }
    }
}