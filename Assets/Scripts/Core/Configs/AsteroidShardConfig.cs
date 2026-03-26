using System;

namespace Core.Configs
{
    [Serializable]
    public struct AsteroidShardConfig
    {
        public int MaxHealth;
        public int CollisionDamage;
        public float Speed;
        public float BounceSpeed;
    }
}