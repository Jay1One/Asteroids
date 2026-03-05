using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units.Enemies.Movement
{
    public abstract class EnemyMovementService
    {
        protected readonly PhysicsBody PhysicsBody;
        protected readonly Transform Transform;

        public EnemyMovementService(PhysicsBody physicsBody, Transform transform)
        {
            PhysicsBody = physicsBody;
            Transform = transform;
        }
        
        public abstract void StartMoving();

        public virtual void StopMoving()
        {
            PhysicsBody.SetVelocity(Vector2.zero);
        }
    }

}