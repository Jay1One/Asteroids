using System;
using Core.Configs;
using Core.Signals;
using Zenject;

namespace GamePlay.Combat.Units.Player_mechanics
{
    public class PlayerHealthService : IInitializable, IDisposable
    {
        
        private readonly SignalBus _signalBus;
        private readonly Health _health;
        
        public event Action<int, int> HealthChanged;

        public PlayerHealthService(PlayerConfig playerConfig, SignalBus signalBus)
        {
            _health = new Health(playerConfig.MaxHealth);
            _signalBus = signalBus;
            _health.Died += Die;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            HealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
        }

        private void Die()
        {
            _signalBus.Fire(new PlayerDiedSignal());
        }

        public void Dispose()
        {
            _health.Died -= Die;
        }

        public void Initialize()
        {
            HealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}