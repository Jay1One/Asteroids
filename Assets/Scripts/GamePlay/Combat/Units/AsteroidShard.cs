using Core.Configs;
using GamePlay.Physics;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units
{
    public class AsteroidShard : Enemy, IPoolableObject
    {
        private ObjectPool<AsteroidShard> _pool;
        
        [Inject]
        private void Construct(ObjectPool<AsteroidShard> pool, AsteroidShardConfig asteroidShardConfig)
        {
            _pool = pool;
            BounceSpeed = asteroidShardConfig.BounceSpeed;
            MoveSpeed = asteroidShardConfig.Speed;
            CollisionDamage = asteroidShardConfig.CollisionDamage;
            
            Health = new Health(asteroidShardConfig.MaxHealth);
            
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(asteroidShardConfig.Speed, 0);
        }
        
        public void SetDirection(Vector2 direction)
        {
            PhysicsBody.SetVelocity(direction * MoveSpeed);
        }

        public override void Deactivate()
        {
            _pool.Return(this);
        }

        public override void Activate()
        {
            Health.ResetHealth();
            PhysicsBody.SetVelocity(Vector2.zero);
        }
        
        protected override void Die()
        {
            base.Die();
            Deactivate();
        }
    }
}