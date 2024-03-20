using UnityEngine;

namespace Script.Game
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform _mainCameraTrans;
        private Vector3 _lastCameraPosition;
 
        public Vector2 followWeight;
 
        void Start()
        {
            _mainCameraTrans = Camera.main.transform;
            _lastCameraPosition = _mainCameraTrans.position;
        }
        private void LateUpdate()
        {
            ImageFollowCamera();
 
            _lastCameraPosition = _mainCameraTrans.position;
        }
 
        private void ImageFollowCamera()
        {
            var offsetPosition = _mainCameraTrans.position - _lastCameraPosition;
 
            //根据权重调整背景图片的位置
            transform.position += new Vector3(offsetPosition.x * followWeight.x, offsetPosition.y * followWeight.y, 0);
        }
    }
}