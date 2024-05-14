using System;
using System.Collections;
using Script.Game.Getter;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Script.Game.Object.Player.Action
{
    public class Flip : NetworkBehaviour
    {
        private Quaternion _tempY;
        public bool canFlip = true;
        
        private void Turn()
        {
            if (canFlip)
            {
                
                _tempY.y = KeyGetter.PlayerDir.x switch
                {
                    < 0 => 180,
                    > 0 => 0,
                    _ => _tempY.y
                };
                transform.rotation = _tempY;
            }
        }

        private void Update()
        {
            if (IsOwner)
                Turn();
        }
    }
}