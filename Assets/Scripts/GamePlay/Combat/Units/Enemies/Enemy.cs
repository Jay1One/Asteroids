using Core.Interfaces;
using GamePlay.Combat.Units.Enemies.Collision;
using GamePlay.Combat.Units.Enemies.EnemyHealth;
using GamePlay.Combat.Units.Enemies.Movement;
using GamePlay.Physics;
using GamePlay.Pooling;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies
{
    public abstract class Enemy : MonoBehaviour, IPoolableObject, IEnemy
    {
        protected EnemyMovementService EnemyMovementService;
        protected EnemyCollisionService EnemyCollisionService;
        protected EnemyHealthService EnemyHealthService;
        protected PhysicsBody PhysicsBody;
        
        public virtual void Deactivate()
        {
            EnemyMovementService.StopMoving();
        }

        public void Activate()
        {
            EnemyHealthService.ResetHealth();
            EnemyMovementService.StartMoving();
        }

        public void TakeDamage(int damage)
        {
            EnemyHealthService.TakeDamage(damage);
        }
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            EnemyCollisionService.ProcessCollision(other);
        }
    }
}