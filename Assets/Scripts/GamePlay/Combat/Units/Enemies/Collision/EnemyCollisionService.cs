using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies.Collision
{
    public class EnemyCollisionService
    {
        private readonly float _bounceSpeed;
        private readonly PhysicsBody _physicsBody;
        private readonly int _collisionDamage;

        public EnemyCollisionService(float bounceSpeed, int collisionDamage,PhysicsBody physicsBody)
        {
            _bounceSpeed = bounceSpeed;
            _physicsBody = physicsBody;
            _collisionDamage = collisionDamage;
        }
        public void ProcessCollision(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Player_mechanics.Player>(out var player))
            {
                Vector2 direction = other.contacts[0].normal;
                _physicsBody.SetVelocity(direction * _bounceSpeed);
                player.TakeDamage(_collisionDamage);
            }
        }
    }
}