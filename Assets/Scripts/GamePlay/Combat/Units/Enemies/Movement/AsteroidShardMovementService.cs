using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies.Movement
{
    public class AsteroidShardMovementService : EnemyMovementService
    {
        private readonly float _moveSpeed;
        public AsteroidShardMovementService(PhysicsBody physicsBody, 
            Transform transform, float moveSpeed) : base(physicsBody, transform)
        {
            _moveSpeed = moveSpeed;
        }

        public override void StartMoving()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            PhysicsBody.SetVelocity(direction * _moveSpeed);
        }
    }
}