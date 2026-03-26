using GamePlay.Physics;
using UnityEngine;

namespace GamePlay.Combat.Units
{
    [RequireComponent(typeof(PhysicsBody))]
    public abstract class Unit : MonoBehaviour
    {
        protected Health Health;
        protected PhysicsBody PhysicsBody;

        public virtual void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
            if (Health.CurrentHealth==0)
            {
                Die();
            }
        }
        protected abstract void Die();
    }
}