using System;
using UnityEngine;

namespace Script.Game.Object
{
    public class CameraFollow : MonoBehaviour
    {
        private GameObject _lookPosition;
        private Vector3 _tempLookPosition;
        private Vector3 _cameraSpeed=new(10,10,0);
        private void Awake()
        {
            _lookPosition = GameObject.FindGameObjectWithTag("LookPosition");
        }

        private void FixedUpdate()
        {
            var current = gameObject.transform.position;
            var target = _lookPosition.transform.position;
            
            _tempLookPosition = Vector3.SmoothDamp(current, target, ref _cameraSpeed, 1f);
            _tempLookPosition.z = -10;
            transform.position = _tempLookPosition;
        }
    }
}