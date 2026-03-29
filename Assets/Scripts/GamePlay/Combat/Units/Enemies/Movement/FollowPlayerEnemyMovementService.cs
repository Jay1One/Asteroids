using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Player_mechanics;
using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies.Movement
{
    public class FollowPlayerEnemyMovementService : EnemyMovementService, IDisposable
    {
        private readonly Player _player;
        private readonly PlayerState _playerState;
        private readonly GameEndTracker _gameEndTracker;
        private readonly float _moveSpeed;
        private CancellationTokenSource _cts;

        public FollowPlayerEnemyMovementService(PhysicsBody physicsBody, Transform transform, float moveSpeed, Player player,
            PlayerState playerState, GameEndTracker gameEndTracker) : base(physicsBody, transform)
        {
            _moveSpeed = moveSpeed;
            _player = player;
            _playerState = playerState;
            _gameEndTracker = gameEndTracker;
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public override void StartMoving()
        {
            PhysicsBody.SetVelocity(Vector2.zero);
            
            _cts = new CancellationTokenSource();
            _ = ChasePlayerAsync(_cts.Token);
        }

        public override void StopMoving()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private async UniTask ChasePlayerAsync(CancellationToken cancellationToken)
        {
            while (!_gameEndTracker.IsGameOver)
            {
                if (_player!=null && !_playerState.IsInvincible)
                {
                    Vector2 direction =(_player.transform.position - Transform.position).normalized;
                    PhysicsBody.AddForce(direction * (_moveSpeed * Time.deltaTime));
                }
                
                await UniTask.Yield(cancellationToken);
            }
        }
    }
}