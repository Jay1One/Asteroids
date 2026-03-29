using Core.Configs;
using GamePlay.Combat.Units.Enemies.Collision;
using GamePlay.Combat.Units.Enemies.EnemyHealth;
using GamePlay.Combat.Units.Enemies.Movement;
using GamePlay.Physics;
using GamePlay.Pooling;
using Zenject;

namespace GamePlay.Combat.Units.Enemies
{
    public class AsteroidShard : Enemy
    {
        private ObjectPool<AsteroidShard> _pool;
        
        [Inject]
        private void Construct(ObjectPool<AsteroidShard> pool, AsteroidShardConfig asteroidShardConfig)
        {
            _pool = pool;
            
            PhysicsBody = GetComponent<PhysicsBody>();
            
            EnemyHealthService = new EnemyHealthService(asteroidShardConfig.MaxHealth);
            EnemyHealthService.Initialize();
            EnemyHealthService.Died += Deactivate;
            
            EnemyMovementService = new AsteroidShardMovementService(PhysicsBody, transform, asteroidShardConfig.Speed);
            EnemyCollisionService = new EnemyCollisionService(asteroidShardConfig.BounceSpeed, asteroidShardConfig.CollisionDamage, PhysicsBody);
            PhysicsBody.Initialize(asteroidShardConfig.Speed, 0);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _pool.Return(this);
        }

        private void OnDestroy()
        {
            EnemyHealthService.Died -= Deactivate;
            EnemyHealthService.Dispose();
        }
    }
}