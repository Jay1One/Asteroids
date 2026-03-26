using Core.Configs;
using GamePlay.Physics;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GamePlay.Combat.Units
{
    public class Asteroid : Enemy
    {
        private ObjectPool<Asteroid> _pool;
        private ObjectPool<AsteroidShard> _shardPool;
        
        private Vector2 _direction;
        private int _shardsSpawned;
        
        [Inject]
        public void Construct(ObjectPool<Asteroid> pool, ObjectPool<AsteroidShard> shardPool,
            AsteroidConfig asteroidConfig)
        {
            _pool = pool;
            _shardPool = shardPool;
            _shardsSpawned = asteroidConfig.ShardsSpawned;
            MoveSpeed = asteroidConfig.Speed;
            BounceSpeed = asteroidConfig.BounceSpeed;
            CollisionDamage = asteroidConfig.CollisionDamage;
            
            Health = new Health(asteroidConfig.MaxHealth);
            
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(asteroidConfig.Speed, 0);
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
            SpawnShards();
            Deactivate();
        }
        
        private void SpawnShards()
        {
            for (int i = 0; i < _shardsSpawned; i++)
            {
                AsteroidShard asteroidShard = _shardPool.GetObject();
                asteroidShard.transform.position = transform.position;
                asteroidShard.SetDirection(Random.insideUnitCircle.normalized);
            }
        }
    }
}
