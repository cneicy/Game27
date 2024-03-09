using Script.Game.Object.Enemy.Interface;
using UnityEngine;

namespace Script.Game.Object.Enemy
{
    public class Noob : MonoBehaviour , IEnemy
    {
        public int Hp { get; set; } = 10;
        public Rigidbody2D Rigidbody2D { get; set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public int OnDamage(int sourceDamage)
        {
            return Hp -= sourceDamage;
        }

        
    }
}