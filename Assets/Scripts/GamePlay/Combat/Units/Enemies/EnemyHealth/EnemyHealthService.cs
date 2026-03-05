using System;
using Zenject;

namespace GamePlay.Combat.Units.Enemies.EnemyHealth
{
    public class EnemyHealthService : IInitializable, IDisposable
    {
        private readonly Health _health;
        public event Action Died;
        
        public EnemyHealthService(int maxHealth)
        {
            _health = new Health(maxHealth);
        }
        
        public virtual void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public void ResetHealth()
        {
            _health.ResetHealth();
        }

        public void Initialize()
        {
            _health.Died += Die;
        }

        public void Dispose()
        {
            _health.Died -= Die;
        }
        
        private void Die()
        {
            Died?.Invoke();
        }
    }
}