using UnityEngine;

namespace Script.Game.Object.Enemy.Interface
{
    public interface IEnemy
    {
        public int Hp { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public int OnDamage(int sourceDamage);
        
    }
}