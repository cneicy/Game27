using Unity.Netcode;
using UnityEngine;

namespace Script.Game.Object
{
    public class CameraFollow : NetworkBehaviour
    {
        private bool _found;
        private GameObject _lookPosition;
        private Vector3 _tempLookPosition;
        private Vector3 _cameraSpeed = new(10, 10, 0);


        private void FixedUpdate()
        {
            /*
             * 相机平滑跟随玩家
             */
            try
            {
                var temp = GameObject.FindGameObjectsWithTag("LookPosition");
                foreach (var point in temp)
                {
                    if (point.GetComponent<LookPosition>().IsLocalPlayer)
                    {
                        _lookPosition = point;
                    }
                }

                var current = transform.position;
                var target = _lookPosition.transform.position;

                _tempLookPosition = Vector3.SmoothDamp(current, target, ref _cameraSpeed, 0.15f);
                _tempLookPosition.z = -10;
                transform.position = _tempLookPosition;
            }
            catch
            {
            }
        }
    }
}