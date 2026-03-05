using System;

namespace Core.Configs
{
    [Serializable]
    public struct AsteroidConfig
    {
        public int MaxHealth;
        public int CollisionDamage;
        public float Speed;
        public float BounceSpeed;
        public int ShardsSpawned;
    }
}