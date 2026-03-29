using Core.Configs;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Enemies.Collision;
using GamePlay.Combat.Units.Enemies.EnemyHealth;
using GamePlay.Combat.Units.Enemies.Movement;
using GamePlay.Combat.Units.Player_mechanics;
using GamePlay.Physics;
using GamePlay.Pooling;
using Zenject;

namespace GamePlay.Combat.Units.Enemies
{
    public class Ufo : Enemy
    {
        private ObjectPool<Ufo> _pool;
        
        [Inject]
        public void Construct(ObjectPool<Ufo> pool, UfoConfig ufoConfig, Player_mechanics.Player player,
            PlayerState playerState, GameEndTracker gameEndTracker)
        {
            _pool = pool;
            
            EnemyHealthService = new EnemyHealthService(ufoConfig.MaxHealth);
            EnemyHealthService.Initialize();
            EnemyHealthService.Died += Deactivate;
            
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(ufoConfig.Speed, 0);
            
            EnemyMovementService = new FollowPlayerEnemyMovementService(PhysicsBody, transform, ufoConfig.Speed, 
                player, playerState,gameEndTracker);
            EnemyCollisionService = new EnemyCollisionService(ufoConfig.BounceSpeed, ufoConfig.CollisionDamage, PhysicsBody);
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