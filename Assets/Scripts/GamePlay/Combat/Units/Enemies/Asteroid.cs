using Core.Configs;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Enemies.Collision;
using GamePlay.Combat.Units.Enemies.EnemyHealth;
using GamePlay.Combat.Units.Enemies.Movement;
using GamePlay.Physics;
using GamePlay.Pooling;
using Zenject;

namespace GamePlay.Combat.Units.Enemies
{
    public class Asteroid : Enemy
    {
        private ObjectPool<Asteroid> _pool;
        private ShardSpawner _shardSpawner;
        
        [Inject]
        public void Construct(ObjectPool<Asteroid> pool, ShardSpawner shardSpawner, AsteroidConfig asteroidConfig)
        {
            _pool = pool;
            _shardSpawner = shardSpawner;
            
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(asteroidConfig.Speed, 0);
            
            EnemyHealthService = new EnemyHealthService(asteroidConfig.MaxHealth);
            EnemyHealthService.Initialize();
            EnemyHealthService.Died += OnDeath;
            
            EnemyMovementService = new AsteroidMovementService(PhysicsBody, transform, asteroidConfig.Speed);
            EnemyCollisionService = new EnemyCollisionService(asteroidConfig.BounceSpeed, asteroidConfig.CollisionDamage, PhysicsBody);
        }
        
        public override void Deactivate()
        {
            _pool.Return(this);
        }

        private void OnDestroy()
        {
            EnemyHealthService.Died -= OnDeath;
            EnemyHealthService.Dispose();
        }

        private void OnDeath()
        {
            _shardSpawner.SpawnShards(transform.position);
            Deactivate();
        }
    }
}
