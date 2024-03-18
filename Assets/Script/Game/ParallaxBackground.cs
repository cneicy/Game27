using UnityEngine;

namespace Script.Game
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform mainCameraTrans; // 主摄像机的Transform组件
        private Vector3 lastCameraPosition; // 上一帧摄像机的位置
        private float textureUnitSizeX; // 背景图单位尺寸
 
        public Vector2 followWeight;
 
        void Start()
        {
            mainCameraTrans = Camera.main.transform; // 获取主摄像机的Transform组件
            lastCameraPosition = mainCameraTrans.position; // 初始化上一帧摄像机的位置为当前摄像机的位置
 
            Sprite sprite = GetComponent<SpriteRenderer>().sprite; 
            Texture2D texture = sprite.texture; // 获取Sprite的纹理
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit; // 计算背景图在游戏场景里的单位尺寸
        }
        private void LateUpdate()
        {
            ImageFollowCamera();
            //ResetImageX();
 
            lastCameraPosition = mainCameraTrans.position; // 更新上一帧摄像机的位置
        }
 
        private void ResetImageX()
        {
            // 检查是否需要移动背景
            if (Mathf.Abs(mainCameraTrans.position.x - transform.position.x) >= textureUnitSizeX)
            {
                // 重置背景位置
                transform.position = new Vector3(mainCameraTrans.position.x , transform.position.y, transform.position.z);
            }
        }
 
        private void ImageFollowCamera()
        {
            // 计算摄像机位置的偏移量
            Vector3 offsetPosition = mainCameraTrans.position - lastCameraPosition;
 
            // 根据权重调整背景图片的位置
            transform.position += new Vector3(offsetPosition.x * followWeight.x, offsetPosition.y * followWeight.y, 0);
        }
    }
}