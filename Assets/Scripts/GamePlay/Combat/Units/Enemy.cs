using Core.Interfaces;
using Core.Signals;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units
{
    public abstract class Enemy : Unit, IPoolableObject, IEnemy
    {
        private SignalBus _signalBus;
        protected float MoveSpeed;
        protected float BounceSpeed;
        protected int CollisionDamage;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public abstract void Deactivate();
        public abstract void Activate();
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Player>( out var player))
            {
                player.TakeDamage(CollisionDamage);
                
                Vector2 direction = other.contacts[0].normal;
                PhysicsBody.SetVelocity(direction * BounceSpeed);
            }
        }

        protected override void Die()
        {
            _signalBus.Fire(new EnemyDiedSignal(this));
        }
    }
}