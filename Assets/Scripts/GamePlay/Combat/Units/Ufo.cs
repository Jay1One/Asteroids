using System.Threading;
using Core.Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Combat.Systems;
using GamePlay.Physics;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units
{
    public class Ufo : Enemy, IPoolableObject
    {
        private Player _player;
        private ObjectPool<Ufo> _pool;
        private CancellationTokenSource _cts;
        private GameEndTracker _gameEndTracker;
        
        [Inject]
        public void Construct(ObjectPool<Ufo> pool, UfoConfig ufoConfig, Player player, GameEndTracker endTracker)
        {
            _pool = pool;
            _player = player;
            Health = new Health(ufoConfig.MaxHealth);
            MoveSpeed = ufoConfig.Speed;
            BounceSpeed = ufoConfig.BounceSpeed;
            CollisionDamage = ufoConfig.CollisionDamage;
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(ufoConfig.Speed, 0);
            _gameEndTracker = endTracker;
        }
        
        protected override void Die()
        {
            base.Die();
            Deactivate();
        }

        private async UniTask ChasePlayerAsync(CancellationToken cancellationToken)
        {
            while (!_gameEndTracker.IsGameOver)
            {
                if (_player!=null && !_player.IsInvincible)
                {
                    Vector2 direction =(_player.transform.position - transform.position).normalized;
                    PhysicsBody.AddForce(direction * (MoveSpeed * Time.deltaTime));
                }
                
                await UniTask.Yield(cancellationToken);
            }
        }

        public override void Deactivate()
        {
            _cts?.Cancel();
            _pool.Return(this);
        }

        public override void Activate()
        {
            Health.ResetHealth();
            PhysicsBody.SetVelocity(Vector2.zero);
            _cts = new CancellationTokenSource();
            _ = ChasePlayerAsync(_cts.Token);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            _cts?.Cancel();
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
        }
    }
}