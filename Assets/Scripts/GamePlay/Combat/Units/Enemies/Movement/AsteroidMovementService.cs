using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies.Movement
{
    public class AsteroidMovementService : EnemyMovementService
    {
        private readonly float _randomRotationRange = 30f;
        private readonly float _moveSpeed;
        public AsteroidMovementService(PhysicsBody physicsBody,
            Transform transform, float moveSpeed) : base(physicsBody, transform)
        {
            _moveSpeed = moveSpeed;
        }

        public override void StartMoving()
        {
            Vector2 direction = -1f * Transform.position.normalized;
            float randomRotation = Random.Range(-_randomRotationRange, _randomRotationRange);
            var rotation = Quaternion.Euler(0f, 0f, randomRotation);
            direction=rotation * direction;
            PhysicsBody.SetVelocity(direction * _moveSpeed);
        }
    }
}